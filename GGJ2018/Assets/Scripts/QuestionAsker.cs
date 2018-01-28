using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class QuestionAsker : MonoBehaviour {

    public List<Text> questions;
    public List<List<Text>> answers;
    public int numberOfQuestions;

    private int maxpoolnumber;
    private List<string> questionPool;
    private List<List<string>> answerPool;

	// Use this for initialization
	void Start () {
        List<Text> auxlist = new List<Text>();
        questionPool = new List<string>();
        answerPool = new List<List<string>>();

        answers = new List<List<Text>>();
        for(int i = 0; i < 6; i++) {
            Text aux = null;

            if (i == 0) {
                aux = GameObject.Find("Answer00").transform.Find("Text").GetComponent<Text>();
                auxlist.Add(aux);
            }
            if (i == 1) {
                aux = GameObject.Find("Answer01").transform.Find("Text").GetComponent<Text>();
                auxlist.Add(aux);
            }
            if (i == 2) {
                aux = GameObject.Find("Answer02").transform.Find("Text").GetComponent<Text>();
                auxlist.Add(aux);
                answers.Add(auxlist);
            }

            if (i == 3) {
                auxlist = new List<Text>();
                aux = GameObject.Find("Answer10").transform.Find("Text").GetComponent<Text>();
                auxlist.Add(aux);
            }
            if (i == 4) {
                aux = GameObject.Find("Answer11").transform.Find("Text").GetComponent<Text>();
                auxlist.Add(aux);
            }
            if (i == 5) {
                aux = GameObject.Find("Answer12").transform.Find("Text").GetComponent<Text>();
                auxlist.Add(aux);
                answers.Add(auxlist);
            }
        }

        ReadFromFile();
        GenerateQuestionsAndAnswers();
        PlaceRewards();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void ReadFromFile() {
        string wholeText = null;
        char delimiter = ';';
        string[] brokenText;
        int cont = 0;
        List<string> aux;

        aux = new List<string>();

        try {
            using (StreamReader sr = new StreamReader("Assets/fileData/QA.txt")) {
                wholeText = sr.ReadToEnd();
            }
        }
        catch (Exception e) {
            UnityEngine.MonoBehaviour.print("Error: Couldn't read 'QA.txt' file. Could it be missing or corrupted?\nException:\n");
            UnityEngine.MonoBehaviour.print(e.Message);
        }

        wholeText = wholeText.Replace(System.Environment.NewLine, "");

        brokenText = wholeText.Split(delimiter);
        foreach (var campo in brokenText) {
            if(cont == 0) {
                maxpoolnumber = Int32.Parse(campo);
            }
            if(cont == 1) {
                questionPool.Add(campo);
                aux = new List<string>();
                answerPool.Add(aux);
            }
            if(cont >= 2 && cont <= 4) {
                aux.Add(campo);
            }
            cont++;
            if(cont > 4) {
                cont = 1;
            }
        }
    }

    private void GenerateQuestionsAndAnswers() {
        List<int> markedAnswers = new List<int>();
        List<int> markedQuestions = new List<int>();
        int rand0, rand1;
        rand0 = UnityEngine.Random.Range(0, maxpoolnumber);
        rand1 = UnityEngine.Random.Range(0, maxpoolnumber);
        for (int i = 0; i < questions.Count; i++) {
            while (markedQuestions.Contains(rand0)) {
                print("randomInvalido");
                rand0 = UnityEngine.Random.Range(0, maxpoolnumber);
            }
            questions[i].text = questionPool[rand0];
            markedQuestions.Add(rand0);
            for(int j = 0; j < 3; j++) {
                while (markedAnswers.Contains(rand1))
                    rand1 = UnityEngine.Random.Range(0, maxpoolnumber);
                answers[i][j].text = answerPool[rand0][rand1];
                markedAnswers.Add(rand1);
            }

            markedAnswers = new List<int>();
        }
    }

    private void PlaceRewards() {

    }
}
