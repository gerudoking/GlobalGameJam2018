using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempAnimation : MonoBehaviour {
    public float delay = 0f;

    // Use this for initialization
    void Start() {
        print("Apareci");
        Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay);
    }
}
