using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Class used for the players movement
public class PlayerMove : MonoBehaviour {

    public float bonusTime = 10;
    public bool isPlayer1;

    public float speed;//The players horizontal movement speed
    public float jumpSpeed; //How fast is the players jump(resulting in its height)
    public float secondJumpSpeed;
    public float gravity;//The gravity thats applied over the player
    public float fallSpeedMax;//The maximum falling speed that can be achieved
    public float invurenabilityTime;
    public float timeAdd;
    public float timeJump;
    public float blinkSpeed;
    public float startTime;

    public int presentQuestion = 0;

    public BoxCollider2D all;
    public BoxCollider2D front;

    public bool isAnswering;
    public List<GameObject> answerZ;
    public List<GameObject> answerX;
    public List<GameObject> answerC;

    public int questionNumber = 0;

    private bool jumping = false; //If the player is jumping or not
    private bool falling = false; //If the player is falling or not
    private bool running = false;
    private bool secondJump = false;
    private float fallSpeed = 0;
    private Rigidbody2D rb;
    private Vector2 lastPos;
    private Vector2 speedVec;
    private Vector2 lastHorPos;
    private bool blinkingToZ = false;
    private bool blinkingToX = false;
    private bool blinkingToC = false;
    public float timerJump;
    public bool started = false;
    private Animator anim;
    private AnimationClip[] clips;

    private float jumpAnimTime;
    private float runAnimTime;
    private float fallAnimTime;
    private float idleAnimTime;

    private float jumpAnimTimer = 0;
    private float fallAnimTimer = 0;

    //private BoxCollider2D collider;

    // Use this for initialization
    void Start () {
        //collider = gameObject.GetComponent<BoxCollider2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        timerJump = timeJump;
        isAnswering = false;
        anim = gameObject.GetComponent<Animator>();
        clips = anim.runtimeAnimatorController.animationClips;
    }
	
	// Update is called once per frame
	void Update () {
        //transform.Translate(Vector3.right * (1 * Time.deltaTime), Space.World);
        /*speedVec = rb.velocity;
        speedVec.x = speed * Time.deltaTime;
        rb.velocity = speedVec;*/

        //Update animations
        foreach (AnimationClip clip in clips) {
            if (isPlayer1) {
                switch (clip.name) {
                    case "char0_jumping":
                        jumpAnimTime = clip.length;
                        break;
                    case "char0_falling":
                        fallAnimTime = clip.length;
                        break;
                }
            }
            else {
                switch (clip.name) {
                    case "char1_jumping":
                        jumpAnimTime = clip.length;
                        break;
                    case "char1_falling":
                        fallAnimTime = clip.length;
                        break;
                }
            }
        }

        //Check for input
        if (started == true) {
            InputCheck();

            if (jumping == true && falling == false && running == false) {
                jumpAnimTimer -= Time.deltaTime;
                if (jumpAnimTimer <= 0) {
                    anim.speed = 0;
                }
            }
            else if(jumping == true && falling == true && running == false) {
                fallAnimTimer -= Time.deltaTime;
                if(fallAnimTimer > 0) {
                    if (isPlayer1) {
                        anim.Play("char0_falling");
                    }
                    else {
                        anim.Play("char1_falling");
                    }
                    anim.speed = 1;
                }
                else {
                    anim.speed = 0;
                }
            }
            else if(jumping == false && falling == false && running == true) {
                if (isPlayer1) {
                    anim.Play("char0_running");
                    anim.speed = 1;
                }
                else {
                    anim.Play("char1_running");
                    anim.speed = 1;
                }
            }

            if (blinkingToZ || blinkingToX || blinkingToC) {
                rb.simulated = false;
            }
            else {
                //transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
                speedVec = rb.velocity;
                speedVec.x = speed * Time.deltaTime;
                rb.velocity = speedVec;
                rb.simulated = true;
            }

            if (lastPos.y > transform.position.y && falling == false) {
                rb.AddForce(new Vector2(0, -fallSpeedMax), ForceMode2D.Impulse);
                fallAnimTimer = fallAnimTime;
                falling = true;
            }

            lastPos = transform.position;
            invurenabilityTime -= Time.deltaTime;
            //if(jumping == true)
            timerJump -= Time.deltaTime;

            if (timerJump <= 0) {
                rb.gravityScale = 100;
            }

            if (blinkingToZ) {
                transform.position = Vector3.MoveTowards(transform.position, answerZ[questionNumber].transform.position, blinkSpeed * Time.deltaTime);
                if (transform.position == answerZ[questionNumber].transform.position) {
                    blinkingToZ = false;
                    isAnswering = false;
                    questionNumber++;
                }
            }

            if (blinkingToX) {
                transform.position = Vector3.MoveTowards(transform.position, answerX[questionNumber].transform.position, blinkSpeed * Time.deltaTime);
                if (transform.position == answerX[questionNumber].transform.position) {
                    blinkingToX = false;
                    isAnswering = false;
                    questionNumber++;
                }
            }

            if (blinkingToC) {
                transform.position = Vector3.MoveTowards(transform.position, answerC[questionNumber].transform.position, blinkSpeed * Time.deltaTime);
                if (transform.position == answerC[questionNumber].transform.position) {
                    blinkingToC = false;
                    isAnswering = false;
                    questionNumber++;
                }
            }
            /*if(falling == true) {
                rb.gravityScale = 100;
            }*/
        }
        else {
            startTime -= Time.deltaTime;
            if(startTime <= 0) {
                started = true;
                if(isPlayer1)
                    gameObject.GetComponent<Animator>().Play("char0_running");
                else
                    gameObject.GetComponent<Animator>().Play("char1_running");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Player") {
            gameObject.GetComponent<PlayerMove>().enabled = false;
        }
        if(collision.gameObject.tag == "Block") {
            running = true;
        }
        if(collision.gameObject.tag == "endgame") {
            SceneManager.LoadScene(0);
        }
    }

    private void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.tag == "Block") {
            falling = false;
            jumping = false;
            secondJump = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "ExtraTime") {
            print("Quiz time!");
            isAnswering = true;
        }
        if(collision.gameObject.tag == "Overtime") {
            if (!IsBlinking()) {
                int rand = UnityEngine.Random.Range(0, 2);
                switch (rand) {
                    case 0:
                        blinkingToZ = true;
                        break;
                    case 1:
                        blinkingToX = true;
                        break;
                    case 2:
                        blinkingToC = true;
                        break;
                }
                print("Too late!");

            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if(collision.gameObject.tag == "Block") {
            /*if(jumping == false && timerJump == timeJump) {
                rb.gravityScale = 100;
            }*/
            //timerJump -= 1000;
        }
    }

    //Check if a input was made. If true, execute logic
    private void InputCheck() {
        if (Input.GetKeyDown(KeyCode.Space) && isAnswering == false) {
            if (jumping == false && secondJump == false) {
                print("AAA");
                timerJump = timeJump;
                rb.gravityScale = 0;
                running = false;
                jumping = true;
                jumpAnimTimer = jumpAnimTime;
                if(isPlayer1)
                    anim.Play("char0_jumping");
                else
                    anim.Play("char1_jumping");
                rb.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
            }
            if(jumping == true && secondJump == false) {
                secondJump = true;
                rb.AddForce(new Vector2(0, secondJumpSpeed), ForceMode2D.Impulse);
            }
        }

        if (Input.GetKeyDown(KeyCode.Z) && isAnswering == true && IsBlinking() == false) {
            blinkingToZ = true;
            if (GetCorrectAnswer(0))
                GameObject.Find("GameManager").GetComponent<TimeController>().time += bonusTime;
        }

        if (Input.GetKeyDown(KeyCode.X) && isAnswering == true && IsBlinking() == false) {
            blinkingToX = true;
            if (GetCorrectAnswer(1))
                GameObject.Find("GameManager").GetComponent<TimeController>().time += bonusTime;
        }

        if (Input.GetKeyDown(KeyCode.C) && isAnswering == true && IsBlinking() == false) {
            blinkingToC = true;
            if (GetCorrectAnswer(2))
                GameObject.Find("GameManager").GetComponent<TimeController>().time += bonusTime;
        }
    }

    public bool IsBlinking() {
        return blinkingToZ || blinkingToX || blinkingToC;
    }

    private bool GetCorrectAnswer(int number) {
        QuestionAsker asker;
        asker = GameObject.Find("GameManager").GetComponent<QuestionAsker>();

        if (asker.correctAnswer[questionNumber] == number)
            return true;

        return false;
    }
}
