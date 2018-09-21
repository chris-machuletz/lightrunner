using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GegnerScript : MonoBehaviour {
    //Hier wird die Reaktion der Gegner mit dem Schuss verfolgt

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
    }
}
