using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModulePlacer : MonoBehaviour {

    enum ModulePool {MODULE0, MODULE1, MODULE2};
    List<ModulePool> selectedModules;

    public List<GameObject> modules;

	// Use this for initialization
	void Start () {
        selectedModules = new List<ModulePool>();
        SelectModules(0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void SelectModules(int stage) {
        int rnd;

        if (stage == 0) {
            for (int i = 0; i < 8; i++) {
                rnd = UnityEngine.Random.Range(0, 2);
                switch (rnd) {
                    case 0:
                        selectedModules.Add(ModulePool.MODULE0);
                        break;
                    case 1:
                        selectedModules.Add(ModulePool.MODULE1);
                        break;
                    case 2:
                        selectedModules.Add(ModulePool.MODULE2);
                        break;
                }
            }
        }
    }
}
