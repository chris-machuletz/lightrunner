using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProps : MonoBehaviour {

    public int lifes; // Anzahl der Leben des Spielers
    public float lumen; // Anzahl der Lumen des Spielers

	// Use this for initialization
	void Start () {
        lifes = 0;
        lumen = 150.0f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
