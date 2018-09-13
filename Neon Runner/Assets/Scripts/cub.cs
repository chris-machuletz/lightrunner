using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class cub : MonoBehaviour {

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {	
	}

    public GameObject Create()
    {
        GameObject cu = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cu.transform.parent = GameObject.Find("Obstacles").transform;
        cu.name = "Obstacle";
        cu.GetComponent<BoxCollider>().isTrigger = true;
        return cu;
    }
}
