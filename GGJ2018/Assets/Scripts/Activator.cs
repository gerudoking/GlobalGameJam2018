using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour {
    public GameObject prefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision) {
        GameObject aux;
        if (collision.gameObject.tag == "Player") {
            gameObject.GetComponent<PlayerMove>().enabled = true;
            gameObject.GetComponent<PlayerMove>().started = true;
            if(gameObject.GetComponent<PlayerMove>().isPlayer1)
                gameObject.GetComponent<Animator>().Play("char0_running");
            else
                gameObject.GetComponent<Animator>().Play("char1_running");
            aux = Instantiate(prefab, transform);
            aux.transform.position = transform.position;
        }
    }
}
