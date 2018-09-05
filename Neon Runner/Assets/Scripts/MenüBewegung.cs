using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenüBewegung : MonoBehaviour {

    public float n = 1f;

    // Use this for initialization
    void Start()
    {
        transform.Rotate(20, 0, 0);
    }

    void FixedUpdate()
    {
        n = n + 1;
        transform.position = new Vector3(0, 10, n);

    }
}
