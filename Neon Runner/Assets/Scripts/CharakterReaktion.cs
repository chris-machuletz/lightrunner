//test.cs zuständig für den Collider bzw Trigger
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharakterReaktion : MonoBehaviour {

    public HoverLeiste hoverleiste;   //ermöglicht den Zugriff auf die Bool im anderen C#

    //einsammel sound
    public AudioClip music;
    public AudioClip kollision;
    public AudioSource quelle { get { return GetComponent<AudioSource>(); } }

   // private Scene scene;

    private void Start()
    {
       //scene = SceneManager.GetActiveScene();
    }

    void OnTriggerEnter(Collider collisionInfo)
    {
        if (collisionInfo.tag == "HoverUp")   //tag oder name
        {
            quelle.PlayOneShot(music);  //einsammel sound

            hoverleiste.aktuellHover = hoverleiste.aktuellHover + 20.0f;    //für jedes hoverup gibt es + 20 hoverpkt
            HoverUp.gegessen = true;
            if (hoverleiste.aktuellHover > 100)
            {
                hoverleiste.aktuellHover = 100;
            }
        }
        if (collisionInfo.name == "Abgrund")   //tag oder name
        {
            //Application.LoadLevel(4);
            SceneManager.LoadScene(4);
        }
        if (collisionInfo.tag == "Feind")   //tag oder name
        {
            if (GameObject.Find("Ship").GetComponent<PlayerProps>().lifes > 0) // Wenn noch leben vorhanden sind, ziehe eins ab und führe Spiel fort
            {
                //StartCoroutine(PitchBackgroundSound());
                GameObject.Find("Ship").GetComponent<PlayerProps>().lifes--;
                GameObject.Find("Ship").GetComponent<PlayerProps>().setLifeCubes();
                GetComponent<AudioSource>().PlayOneShot(kollision);

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

                SceneManager.LoadScene(4);
            }
        }
    }
}

//Programmierer Alex