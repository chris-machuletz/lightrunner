using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class cub : MonoBehaviour {

    public GameObject[] obstacles;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {	
	}

    public GameObject Create()
    {
        float ransc = Random.Range(100, 800); //setzt Raum für zufällige Skalierung fest (wobei 100 = normalgröße)
        GameObject cu;
        int rndIndex = Random.Range(0, obstacles.Length);
        //GameObject cu = GameObject.CreatePrimitive(PrimitiveType.Cube);


        cu = Instantiate(obstacles[rndIndex]) as GameObject; //erzeugt ein hindernis aus dem array befüllt mit den einzelnen Hindernissen
        cu.transform.parent = GameObject.Find("Obstacles").transform;
        cu.name = "Obstacle";
        cu.AddComponent<MeshCollider>(); //fügt collider hinzu
        cu.GetComponent<MeshCollider>().convex = true;
        cu.GetComponent<MeshCollider>().isTrigger = true; //fügt trigger hinzu
        cu.transform.localScale = new Vector3(ransc, ransc, ransc); //skaliert die obstacles zufällig
        cu.transform.Rotate(0 , 0, Random.Range(0, 180)); //dreht die Obstacles zufällig


        return cu;
    }
}
