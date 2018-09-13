using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProps : MonoBehaviour {

    public int lifes = 0; // Anzahl der Leben des Spielers
    public float lumen = 150.0f; // Anzahl der Lumen des Spielers
    public GameObject selectedShip;
    private GameObject life1;
    private GameObject life2;
    private GameObject life3;
    public bool isAlive;

    // Use this for initialization
    void Start () {
        life1 = GameObject.Find("Life 1");
        life2 = GameObject.Find("Life 2");
        life3 = GameObject.Find("Life 3");
        setLifeCubes();
        life1.SetActive(false);
        life2.SetActive(false);
        life3.SetActive(false);

        isAlive = true;
    }

    // Update is called once per frame
    void Update () {

	}

    public void setLifeCubes()
    {
        if (lifes == 0)
        {
            life1.SetActive(false);
            life2.SetActive(false);
            life3.SetActive(false);
        }
        if (lifes == 1)
        {
            life1.SetActive(true);
            life2.SetActive(false);
            life3.SetActive(false);
        }
        if (lifes == 2)
        {
            life1.SetActive(true);
            life2.SetActive(true);
            life3.SetActive(false);
        }
        if (lifes == 3)
        {
            life1.SetActive(true);
            life2.SetActive(true);
            life3.SetActive(true);
        }
    }
}
