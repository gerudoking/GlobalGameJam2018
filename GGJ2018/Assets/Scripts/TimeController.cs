using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeController : MonoBehaviour {

    public float time;
    public float startingTime;
    public Text timeText;
    public Text countdown;

	// Use this for initialization
	void Start () {
        startingTime = time;
    }
	
	// Update is called once per frame
	void Update () {
        timeText.text = time.ToString("0.0");

        if(time < startingTime - 1) {
            countdown.text = "2";
        }

        if (time < startingTime - 2) {
            countdown.text = "1";
        }

        if (time < startingTime - 3) {
            countdown.text = "think!";
        }

        if (time <= 0) {
            SceneManager.LoadScene(2);
        }

        time -= Time.deltaTime;
	}
}
