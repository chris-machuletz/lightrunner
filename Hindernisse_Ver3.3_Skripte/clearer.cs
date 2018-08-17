using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clearer : MonoBehaviour {

    randomSpawn sr;
    public float starttime = 2f;
    public float time = 15f;
    public int i;


    // Use this for initialization
    void Start () {
        //i = 0;
        InvokeRepeating("Clear", starttime, time); //löst sich ab 2 sek alle 15 sek aus
	}

    public void Clear()
    {
        sr = GetComponent<randomSpawn>(); //Script-Referenz auf randomSpawn (wo die Cubes erzeugt werden)

        // macht unfug
        //for (int j = i; j < sr.obstcount; j++)
        //{
        //    sr.obstacls.Remove(sr.obstacls[j]);
        //    Destroy(sr.obstacls[j]);
        //    i++;
        //}

        //BESTER WEG BISHER:

        //funktioniert nur wenn fast alle gelöscht werden sollen bis zu best. Zeitpunkt
        for (int k = 1; k >= 0; k++)
        {
            Destroy(sr.obstacls[k]);
            sr.obstacls.Remove(sr.obstacls[k]);
        }
        //Debug.Log("DELETED");
    }

	// Update is called once per frame
	void Update () {
		
	}
}
