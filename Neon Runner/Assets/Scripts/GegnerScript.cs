using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GegnerScript : MonoBehaviour {
    //Hier wird die Reaktion der Gegner mit dem Schuss verfolgt

    private Gegner gegner;
    public AudioClip kollision;

    private void Start()
    {
        gameObject.AddComponent<AudioSource>();
        kollision = GameObject.Find("Ship").GetComponent<Lumen>().kollision;
        gegner = GameObject.Find("GegnerSpawner").GetComponent<Gegner>();

        //änderung wegen sound (,,,)
        GetComponent<AudioSource>().clip = kollision;
    }

    void OnTriggerEnter(Collider collisionInfo)
    {
        if (gameObject.name == "Feind1")
        {
            if (collisionInfo.tag == "Schuss")   //tag oder name
            {
                gegner.tot1 = true;
            }
        }
        if (gameObject.name == "Feind2")
        {
            if (collisionInfo.tag == "Schuss")   //tag oder name
            {
                gegner.tot2 = true;
            }
        }
        if (gameObject.name == "Feind3")
        {
            if (collisionInfo.tag == "Schuss")   //tag oder name
            {
                gegner.tot3 = true;
            }
        }
        if (gameObject.name == "Feind4")
        {
            if (collisionInfo.tag == "Schuss")   //tag oder name
            {
                gegner.tot4 = true;
            }
        }
        if (gameObject.name == "Feind5")
        {
            if (collisionInfo.tag == "Schuss")   //tag oder name
            {
                gegner.tot5 = true;
            }
        }
        //Raumschiff berührt
        if (collisionInfo.name == "Ship")
        {
            if (GameObject.Find("Ship").GetComponent<PlayerProps>().lifes > 0) // Wenn noch leben vorhanden sind, ziehe eins ab und führe Spiel fort
            {
                //StartCoroutine(PitchBackgroundSound());
                if (gameObject.name == "Feind1")
                {
                    gegner.tot1 = true;
                }
                if (gameObject.name == "Feind2")
                {
                    gegner.tot2 = true;
                }
                if (gameObject.name == "Feind3")
                {
                    gegner.tot3 = true;
                }
                if (gameObject.name == "Feind4")
                {
                    gegner.tot4 = true;
                }
                if (gameObject.name == "Feind5")
                {
                    gegner.tot5 = true;
                }
                GameObject.Find("Ship").GetComponent<PlayerProps>().lifes--;
                GameObject.Find("Ship").GetComponent<PlayerProps>().setLifeCubes();

                if ((GameObject.Find("Ship").GetComponent<CharakterSteuerung>().vorwärtsspeed * 0.75f) <= 50) //Berechnung der neuen Spielergeschwindigkeit
                {
                    GameObject.Find("Ship").GetComponent<CharakterSteuerung>().vorwärtsspeed = 50;
                }
                else
                {
                    GameObject.Find("Ship").GetComponent<CharakterSteuerung>().vorwärtsspeed *= 0.75f;
                }
            }
            else // Wenn keine Leben mehr vorhanden sind, ist das Spiel zu Ende
            {

                SceneManager.LoadScene(5);
            }
        }
    }
}
