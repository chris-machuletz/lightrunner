using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HauptmenüBewegung : MonoBehaviour {
    float n = 0;

    // Use this for initialization
    void Start () {
        this.transform.position = new Vector3( 0, 10, 0);
        this.transform.Rotate(18, 0, 0);

    }
	
	// Update is called once per frame
	void FixedUpdate() {
        this.transform.position = new Vector3(0, 10, n);
        n = n + 0.5f;
    }
}
