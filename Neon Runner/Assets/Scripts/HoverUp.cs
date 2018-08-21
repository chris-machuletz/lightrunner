using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter)), RequireComponent(typeof(MeshRenderer))]
public class HoverUp : MonoBehaviour
{


    public Mesh mesh;

    public static bool gegessen;

    public GameObject spieler;
    public GameObject cube;

    private int entfernung = 1000;  //legt fest in bis zu welcher entfernung der cube spawnen soll

    void Start()
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();

        gegessen = false;

        this.transform.position = new Vector3(Random.Range(-3.0f, 3.0f), 1, Random.Range(GameObject.FindGameObjectWithTag("Player").transform.position.z, GameObject.FindGameObjectWithTag("Player").transform.position.z + entfernung)); //random spawn für cube

        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.localScale = new Vector3(1.0f, 1.0f, 2.0f);
        cube.GetComponent<Renderer>().material.color = new Color(0.0f, 1.0f, 0.0f);
        cube.transform.position = this.transform.position;

        cube.tag = "HoverUp";
        BoxCollider box = cube.GetComponent(typeof(BoxCollider)) as BoxCollider;    //aktiviert den collider im cube
        box.isTrigger = true;
    }

    private void Update()
    {
        //soll ausgeführt werden, wenn der block konsumiert wurde.
        if (gegessen == true)
        {
            this.transform.position = new Vector3(Random.Range(-3.0f, 3.0f), 1, Random.Range(GameObject.FindGameObjectWithTag("Player").transform.position.z, GameObject.FindGameObjectWithTag("Player").transform.position.z + entfernung));
            gegessen = false;
        }
        if (GameObject.FindGameObjectWithTag("Player").transform.position.z >= this.transform.position.z)
        {
            this.transform.position = new Vector3(Random.Range(-3.0f, 3.0f), 1, Random.Range(GameObject.FindGameObjectWithTag("Player").transform.position.z, GameObject.FindGameObjectWithTag("Player").transform.position.z + entfernung));
            cube.transform.position = this.transform.position;
        }
    }
 

}