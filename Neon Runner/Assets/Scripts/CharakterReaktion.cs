//test.cs zuständig für den Collider bzw Trigger
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharakterReaktion : MonoBehaviour {

    public HoverLeiste hoverleiste;   //ermöglicht den Zugriff auf die Bool im anderen C#

    private Scene scene;

    private void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    void OnTriggerEnter(Collider collisionInfo)
    {
        if (collisionInfo.tag == "HoverUp")   //tag oder name
        {
            Debug.Log(hoverleiste.aktuellHover);
            hoverleiste.aktuellHover = hoverleiste.aktuellHover + 50.0f;    //für jedes hoverup gibt es + 50 hoverpkt
        }
        if (collisionInfo.tag == "Wand")   //tag oder name
        {

        }
    }
}

//Programmierer Alex