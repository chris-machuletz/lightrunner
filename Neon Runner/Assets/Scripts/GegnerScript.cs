using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GegnerScript : MonoBehaviour {

    private Gegner gegner;

    private void Start()
    {
        gegner = GameObject.Find("GegnerSpawner").GetComponent<Gegner>();
    }

    void OnTriggerEnter(Collider collisionInfo)
    {
        if (gameObject.name == "Feind1")
        {
            if (collisionInfo.tag == "Schuss")   //tag oder name
            {
                Debug.Log("Gegner zerstört");
                gegner.tot1 = true;
            }
        }
        if (gameObject.name == "Feind2")
        {
            if (collisionInfo.tag == "Schuss")   //tag oder name
            {
                Debug.Log("Gegner zerstört");
                gegner.tot2 = true;
            }
        }
        if (gameObject.name == "Feind3")
        {
            if (collisionInfo.tag == "Schuss")   //tag oder name
            {
                Debug.Log("Gegner zerstört");
                gegner.tot3 = true;
            }
        }
        if (gameObject.name == "Feind4")
        {
            if (collisionInfo.tag == "Schuss")   //tag oder name
            {
                Debug.Log("Gegner zerstört");
                gegner.tot4 = true;
            }
        }
        if (gameObject.name == "Feind5")
        {
            if (collisionInfo.tag == "Schuss")   //tag oder name
            {
                Debug.Log("Gegner zerstört");
                gegner.tot5 = true;
            }
        }
        if (collisionInfo.tag == "Player")   //tag oder name
        {
            Debug.Log("Gegner berührt");
            Application.LoadLevel(4);
        }
    }
}
