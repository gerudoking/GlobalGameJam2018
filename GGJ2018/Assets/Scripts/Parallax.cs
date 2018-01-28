using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

    public float proportion;
    public float playerSpeed;

	// Use this for initialization
	void Start () {
        playerSpeed = GameObject.Find("Player").GetComponent<PlayerMove>().speed;
	}
	
	// Update is called once per frame
	void Update () {
        if(GameObject.Find("Player").GetComponent<PlayerMove>().started)
            transform.Translate(new Vector3((playerSpeed/1000) * proportion * Time.deltaTime, 0));
	}
}
