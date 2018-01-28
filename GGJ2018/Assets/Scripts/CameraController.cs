using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float speed;
    public float blinkSpeed;
    public bool isBlinking;

	// Use this for initialization
	void Start () {
        speed = GameObject.Find("Player").GetComponent<PlayerMove>().speed;
        blinkSpeed = GameObject.Find("Player").GetComponent<PlayerMove>().blinkSpeed;
        Vector3 playerPos = new Vector3(GetActivePlayer().transform.position.x + 200, GetActivePlayer().transform.position.y, -10);
        transform.position = playerPos;

    }
	
	// Update is called once per frame
	void Update () {
        if (GetActivePlayer().started == true) {
            Vector3 playerPos;

            transform.rotation = Quaternion.Euler(0, 0, 0);

            if (GetActivePlayer().IsBlinking()) {
                isBlinking = true;
            }
            else {
                isBlinking = false;
            }

            /*if (isBlinking == false) {
                gameObject.transform.parent = GameObject.Find("GameManager").transform;
                transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
            }
            else {
                gameObject.transform.parent = GetActivePlayer().transform;
            }

            if (GetActivePlayer().transform.position.y != transform.position.y) {
                playerPos = new Vector3(transform.position.x, GetActivePlayer().transform.position.y, transform.position.z);
                transform.position = playerPos;
            }*/

            //gameObject.transform.parent = GetActivePlayer().transform;

            playerPos = new Vector3(GetActivePlayer().transform.position.x + 200, GetActivePlayer().transform.position.y, -10);
            transform.position = playerPos;

            if (transform.position.y >= 360) {
                transform.position = new Vector3(transform.position.x, 360, transform.position.z);
            }

            if (transform.position.y <= -235) {
                transform.position = new Vector3(transform.position.x, -235, transform.position.z);
            }
        }
    }

    private PlayerMove GetActivePlayer() {
        if (GameObject.Find("Player").GetComponent<PlayerMove>().isActiveAndEnabled)
            return GameObject.Find("Player").GetComponent<PlayerMove>();
        if (GameObject.Find("Player2").GetComponent<PlayerMove>().isActiveAndEnabled)
            return GameObject.Find("Player2").GetComponent<PlayerMove>();
        return null;
    }
}
