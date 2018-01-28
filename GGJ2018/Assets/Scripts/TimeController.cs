using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour {

    public float time;
    public Text timeText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timeText.text = time.ToString();

        if(time <= 0) {
            print("DEFEAT");
        }

        time -= Time.deltaTime;
	}
}
