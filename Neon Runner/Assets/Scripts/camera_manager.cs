using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_manager : MonoBehaviour {

    public GameObject Zurück;
    public GameObject Weiter;

    // Use this for initialization
    void Start () {
        Cam();
	}
	
    void Cam()
    {
        gameObject.transform.position = new Vector3(5, 3, -22);
        gameObject.transform.Rotate(30, -90, 0);


    }

    Vector3 borVecl = new Vector3(5, 3, - 22);
    Vector3 borVecr = new Vector3(5, 3, 14);

    public void Keyboard()
    {

            if (Input.GetKeyDown("a") || Input.GetKeyDown("left"))
            {
                if (gameObject.transform.position != borVecl) //lässt sich das Objekt so lange nach links bewegen, wie es nicht den Grenzvektor überschreitet
                {
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - 12); //bewegt das Objekt um 10 Einheiten nach links
                }

            }

            if (Input.GetKeyDown("d") || Input.GetKeyDown("right"))
            {
                if (gameObject.transform.position != borVecr) //lässt sich das Objekt so lange nach rechts bewegen, wie es nicht den Grenzvektor überschreitet
                {
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 12); //bewegt das Objekt um 10 Einheiten nach rechts 
                }
            }


    }


    // Update is called once per frame
    void Update () {
        
        Keyboard();

        if (gameObject.transform.position == borVecl)
        {
            Zurück.SetActive(false);
        } else
        {
            Zurück.SetActive(true);
        }

        if (gameObject.transform.position == borVecr)
        {
            Weiter.SetActive(false);
        } else
        {
            Weiter.SetActive(true);
        }
    }
}
