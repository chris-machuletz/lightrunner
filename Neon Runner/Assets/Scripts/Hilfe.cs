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

    // Use this for initialization
    void Start () {

        //hover cube
        HoverCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        HoverCube.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        HoverCube.GetComponent<Renderer>().material = HoverMats;
        HoverCube.transform.position = new Vector3(-8, 18, 0);
        HoverCube.transform.Rotate(25, 0, 0);
        HoverCube.name = "HoverUp";

        lightGameObject1 = new GameObject("The Light");
        Light lightComp1 = lightGameObject1.AddComponent<Light>();
        lightComp1.color = Color.green;
        lightComp1.intensity = 8.5f;
        lightComp1.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
        lightGameObject1.transform.position = HoverCube.transform.position;

        //Leben cube
        LebenCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        LebenCube.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        LebenCube.GetComponent<Renderer>().material = LebenMats;
        LebenCube.transform.position = new Vector3(-8, 18, 0);
        LebenCube.transform.Rotate(25, 0, 0);
        LebenCube.name = "LebenUp";

        lightGameObject2 = new GameObject("The Ligh2t");
        Light lightComp2 = lightGameObject2.AddComponent<Light>();
        lightComp2.color = Color.red;
        lightComp2.intensity = 8.5f;
        lightComp2.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
        lightGameObject2.transform.position = LebenCube.transform.position;

        //lumen cube
        LumenCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        LumenCube.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        LumenCube.GetComponent<Renderer>().material = LumenMats;
        LumenCube.transform.position = new Vector3(-8, 18, 0);
        LumenCube.transform.Rotate(25, 0, 0);
        LumenCube.name = "HoverUp";

        lightGameObject3 = new GameObject("The Light3");
        Light lightComp3 = lightGameObject3.AddComponent<Light>();
        lightComp3.color = Color.white;
        lightComp3.intensity = 8.5f;
        lightComp3.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
        lightGameObject3.transform.position = LumenCube.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
