using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class col_self : MonoBehaviour {

    randomSpawn sr;

    public float ranx, rany, ranz; // achsen-werte die zufällig sein sollen
    public float borderx = 3f; // obere Grenze für zufälligen x-wert
    public float bordery = 0f; // obere Grenze für y-wert
    public float borderz = 5f; //obere Grenze für zufälligen z-wert

    public Vector3 posVec; // Vector für neue Position



    // Use this for initialization
    void Start () {
        BoxCollider bc = gameObject.AddComponent(typeof(BoxCollider)) as BoxCollider;
        bc.isTrigger = true;

        Rigidbody rb = gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;

        sr = GetComponent<randomSpawn>();
    }
	
	// Update is called once per frame
	void Update () {
	}

    public float RandomizeX() // setzt den zu verwendenden x-wert zufällig
    {
        ranx = Random.Range(-borderx, borderx);
        return ranx;
    }

    public float RandomizeY() // setzt den zu verwendenden x-wert zufällig
    {
        rany = Random.Range(0, bordery);
        return rany;
    }

    public float RandomizeZ() // setzt den zu verwendenden z-wert zufällig
    {
        ranz = Random.Range(-borderz, borderz);
        return ranz;
    }

    //verschiebt das Obstacle auf Zufallsposition bei Berührung
    void OnTriggerEnter(Collider other)
    {
        RandomizeX();
        RandomizeY();
        RandomizeZ();
        //speichern von zufälligen Werten in Vector
        posVec = new Vector3(ranx, rany, ranz);
        other.transform.position += posVec;

    }

}
