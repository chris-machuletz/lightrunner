//test.cs zuständig für den Collider bzw Trigger
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharakterReaktion : MonoBehaviour {

    public HoverLeiste hoverleiste;   //ermöglicht den Zugriff auf die Bool im anderen C#

    //einsammel sound
    public AudioClip music;
    public AudioSource quelle { get { return GetComponent<AudioSource>(); } }

    private Scene scene;

    private void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    void OnTriggerEnter(Collider collisionInfo)
    {
        if (collisionInfo.tag == "HoverUp")   //tag oder name
        {
            quelle.PlayOneShot(music);  //einsammel sound

            hoverleiste.aktuellHover = hoverleiste.aktuellHover + 20.0f;    //für jedes hoverup gibt es + 20 hoverpkt
            if (hoverleiste.aktuellHover > 100)
            {
                hoverleiste.aktuellHover = 100;
            }
        }
        if (collisionInfo.name == "Abgrund")   //tag oder name
        {
            Application.LoadLevel(4);
        }
    }
}

//Programmierer Alex