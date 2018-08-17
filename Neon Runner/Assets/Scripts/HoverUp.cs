using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.SceneManagement;

//FUNKTIONIERT NOCH GARNIX!!!!
public class HoverUp : MonoBehaviour {

    bool flagW;
    public static bool restart;
    public static bool essen;

    public Mesh mesh;

    public List<Vector3> vertices;
    public List<int> triangles;

    //Variablen
    public int a = 0;           //zähler für die vertices der snake
    public int i = 0;           // i muss immer zB 4 Felder kleiner sein als a wenn die Snake nur ein Block groß sein soll
    public int zeit = 0;        //zeit für das wachsen der snake
    GameObject turtle;
    GameObject korper;  //test
    public bool korperbol;

    float timeLeft = 1.0f;


    float length = 1f;          //Größe der Einheiten der Snake


    // Use this for initialization
    void Start () {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        //erstellung der turtle und positionierung + aufruf der koch funktion
        turtle = new GameObject("Turtle");
        //korper = new GameObject("korper"); //test

        //Erstellung der Componenten für die Futter Aktion auf der Turtle----------------------------------------------------------------
        Rigidbody rigi = turtle.AddComponent(typeof(Rigidbody)) as Rigidbody;
        BoxCollider box = turtle.AddComponent(typeof(BoxCollider)) as BoxCollider;
        //turtle.AddComponent(typeof(test));

        rigi.useGravity = false;
        box.isTrigger = true;
        //--------------------------------------------------------------------------------------------------------------------------------
        flagW = false;
        restart = false;
        essen = false;

        turtle.transform.Translate(new Vector3(1, 0, 0));
        //korper.transform.position = turtle.transform.position - new Vector3(1, 0, 0);

        InvokeRepeating("Move", 0.3f, 0.3f);    //Ruft die Move Methode auf und bestimmt die schnelligkeit des Repeatings
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
