﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Lumen : MonoBehaviour {

    public Text lumenCountText;
    public Text deathText;
    public AudioClip kollision, lumenCollect, lifeCollect, indestructableCollect;
    private float vel; //geschwindigkeit des Spielers (aus Shipmovement.cs)

    // Use this for initialization
    void Start () {
        vel = GameObject.Find("Ship").GetComponent<CharakterSteuerung>().vorwärtsspeed;
        SetCountText();
        deathText.text = "";

        GetComponent<AudioSource>().playOnAwake = false;
    }

	// Update is called once per frame
	void Update () {
        GameObject.Find("Ship").GetComponent<PlayerProps>().lumen -= Time.deltaTime * (vel / 10);
        SetCountText();
        if (GameObject.Find("Ship").GetComponent<PlayerProps>().lumen <= 0)
        {
            //GameObject.Find("Map Generator").GetComponent<BackgroundTerrain>().clearDict();
            Application.LoadLevel(4);
        }
	}

    void SetCountText()
    {
        int lumenCountInt = (int)GameObject.Find("Ship").GetComponent<PlayerProps>().lumen;
        lumenCountText.text = lumenCountInt.ToString();
    }

    private void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.name == "LumenCube" || collision.gameObject.name == "LumenCube(Clone)")
        {
            Destroy(collision.gameObject);
            GameObject.Find("Ship").GetComponent<PlayerProps>().lumen += Random.Range(5,10);
            GetComponent<AudioSource>().PlayOneShot(lumenCollect);
        }

        if (collision.gameObject.name == "LifeCube" || collision.gameObject.name == "LifeCube(Clone)")
        {
            if (GameObject.Find("Ship").GetComponent<PlayerProps>().lifes < 3)
            {
                Destroy(collision.gameObject);
                GameObject.Find("Ship").GetComponent<PlayerProps>().lifes++;
                GameObject.Find("Ship").GetComponent<PlayerProps>().setLifeCubes();
                AudioSource source = GetComponent<AudioSource>();
                GetComponent<AudioSource>().PlayOneShot(lifeCollect);
            }
        }

        if (collision.gameObject.name == "IndestructableCube" || collision.gameObject.name == "IndestructableCube(Clone)")
        {
            Destroy(collision.gameObject);
            GameObject.Find("Ship").GetComponent<PlayerProps>().lifes = 3;
            GameObject.Find("Ship").GetComponent<PlayerProps>().setLifeCubes();

            AudioSource source = GetComponent<AudioSource>();
            GetComponent<AudioSource>().PlayOneShot(indestructableCollect);

            GameObject.Find("Ship").GetComponent<PowerUpSpawnManager>().Indestructable();
        }

        if (collision.gameObject.name == "Obstacle")
        {
            if (GameObject.Find("Ship").GetComponent<PlayerProps>().lifes > 0) // Wenn noch leben vorhanden sind, ziehe eins ab und führe Spiel fort
            {
                Destroy(collision.gameObject);
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
                
                Application.LoadLevel(4);
            }
        }
    }
}