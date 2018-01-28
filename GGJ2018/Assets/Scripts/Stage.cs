using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Stage : MonoBehaviour {

    public List<GameObject> mapObjects;
    private List<int> module;
    public List<List<int>> moduleList;
    public int width;
    public int height;
    public int depth;
    public Camera main;
    public GameObject blockPrefab;

	// Use this for initialization
	void Start () {
        module = new List<int>();
        mapObjects = new List<GameObject>();
        ReadStageFile();
        CreateMap();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void ReadStageFile() {
        string wholeText = null;
        char delimiter = ',';
        string[] brokenText;
        int cont = 0;

        try {
            using (StreamReader sr = new StreamReader("Assets/fileData/Stage0Module0.txt")) {
                wholeText = sr.ReadToEnd();
            }
        }
        catch (Exception e) {
            UnityEngine.MonoBehaviour.print("Error: Couldn't read 'Stage.txt' file. Could it be missing or corrupted?\nException:\n");
            UnityEngine.MonoBehaviour.print(e.Message);
        }

        wholeText = wholeText.Replace(System.Environment.NewLine, "");

        brokenText = wholeText.Split(delimiter);
        foreach(var campo in brokenText) {
            if (cont == 0) {
                width = Int32.Parse(campo);
            }
            else if(cont == 1) {
                height = Int32.Parse(campo);
            }
            else if(cont == 2) {
                depth = Int32.Parse(campo);
            }
            else if(cont >= 3) {
                module.Add(Int32.Parse(campo) - 1);
            }

            cont++;
        }
    }

    private void CreateMap() {
        int localWidth = 0;
        int localHeight = 0;
        GameObject aux;
        Renderer rend;

        for(int i = 0; i < height; i++) {
            for(int j = 0; j < width; j++) {
                if (module[j + i * width] != -1) {
                    aux = Instantiate(blockPrefab, gameObject.transform, true);
                    rend = aux.GetComponent<Renderer>();
                    aux.transform.position = main.ScreenToWorldPoint(new Vector3(0, 1)) + new Vector3(j * rend.bounds.size.x, -(i * rend.bounds.size.y), 10);
                    mapObjects.Add(aux);
                }
            }
        }
    }
}
