using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hilfe : MonoBehaviour {

    public Mesh mesh;

    //hover
    public Material HoverMats;
    private GameObject HoverCube;
    private GameObject lightGameObject1;

    //leben
    public Material LebenMats;
    private GameObject LebenCube;
    private GameObject lightGameObject2;

    //lumen
    public Material LumenMats;
    private GameObject LumenCube;
    private GameObject lightGameObject3;

    //Unverwundbar
    public Material SafeMats;
    private GameObject SafeCube;
    private GameObject lightGameObject4;

    // Use this for initialization
    void Start () {

        //hover cube
        HoverCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        HoverCube.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        HoverCube.GetComponent<Renderer>().material = HoverMats;
        HoverCube.transform.position = new Vector3(-5, 18, 8);
        HoverCube.transform.Rotate(25, 0, 0);
        HoverCube.name = "HoverUp";

        lightGameObject1 = new GameObject("Green Light");
        Light lightComp1 = lightGameObject1.AddComponent<Light>();
        lightComp1.color = Color.green;
        lightComp1.intensity = 8.5f;
        lightComp1.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
        lightGameObject1.transform.position = HoverCube.transform.position;

        //Leben cube
        LebenCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        LebenCube.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        LebenCube.GetComponent<Renderer>().material = LebenMats;
        LebenCube.transform.position = new Vector3(-5, 16, 8);
        LebenCube.transform.Rotate(25, 0, 0);
        LebenCube.name = "LebenUp";

        lightGameObject2 = new GameObject("Red Light");
        Light lightComp2 = lightGameObject2.AddComponent<Light>();
        lightComp2.color = Color.red;
        lightComp2.intensity = 8.5f;
        lightComp2.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
        lightGameObject2.transform.position = LebenCube.transform.position;

        //Unverwundbar cube
        SafeCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        SafeCube.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        SafeCube.GetComponent<Renderer>().material = SafeMats;
        SafeCube.transform.position = new Vector3(-5, 14, 8);
        SafeCube.transform.Rotate(25, 0, 0);
        SafeCube.name = "SafeUp";

        lightGameObject4 = new GameObject("Blue Light");
        Light lightComp4 = lightGameObject4.AddComponent<Light>();
        lightComp4.color = Color.blue;
        lightComp4.intensity = 8.5f;
        lightComp4.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
        lightGameObject4.transform.position = SafeCube.transform.position;

        //lumen cube
        LumenCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        LumenCube.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        LumenCube.GetComponent<Renderer>().material = LumenMats;
        LumenCube.transform.position = new Vector3(-5, 12, 8);
        LumenCube.transform.Rotate(25, 0, 0);
        LumenCube.name = "LumenUp";

        lightGameObject3 = new GameObject("White Light");
        Light lightComp3 = lightGameObject3.AddComponent<Light>();
        lightComp3.color = Color.white;
        lightComp3.intensity = 8.5f;
        lightComp3.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
        lightGameObject3.transform.position = LumenCube.transform.position;


    }
	
	// Update is called once per frame
	void FixedUpdate () {
        HoverCube.transform.Rotate(0, 1, 0);
        LebenCube.transform.Rotate(0, 1, 0);
        LumenCube.transform.Rotate(0, 1, 0);
        SafeCube.transform.Rotate(0, 1, 0);
    }
}
