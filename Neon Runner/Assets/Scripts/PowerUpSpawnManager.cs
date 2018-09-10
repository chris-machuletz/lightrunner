using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawnManager : MonoBehaviour
{
    int counter = 1;
    private void Update()
    {
        if (counter % 20 == 0)
        {
            //Raffle();
        }
        counter++;

        if (Input.GetKeyDown(KeyCode.E))
        {
            UseALife();
        }
    }

    void GetALife() // Fügt dem Spieler ein Extraleben hinzu, sofern er nicht bereits 3 besitzt
    {
        if (GameObject.Find("Ship").GetComponent<PlayerProps>().lifes <= 3)
        {
            GameObject.Find("Ship").GetComponent<PlayerProps>().lifes++;
            Debug.Log(GameObject.Find("Ship").GetComponent<PlayerProps>().lifes);
        }
    }

    void UseALife()
    {
        if (GameObject.Find("Ship").GetComponent<PlayerProps>().lifes >= 0)
        {
            GameObject.Find("Ship").GetComponent<PlayerProps>().lifes--; // Zieht dem Spieler ein Extraleben ab
            GameObject.Find("Ship").GetComponent<PlayerProps>().setLifeCubes();
            GameObject.Find("Ship").GetComponent<PlayerProps>().lumen += 50; //Fügt dem Spieler 50 Lumen hinzu
            Debug.Log("Used a Life");
        }
    }



    void Raffle() // Jackpot verlosung für Lumen Cubes: 60% Wahrscheinlichkeit, dass Spieler 10% der Cubes verliert, 40%, dass er seine Cubes verfünffacht
    {
        int rnd = (int)Random.Range(1, 5);
        if (rnd <= 3)
        {
            GameObject.Find("Ship").GetComponent<PlayerProps>().lumen *= 0.9f;
        }
        else
        {
            GameObject.Find("Ship").GetComponent<PlayerProps>().lumen *= 2f;
        }
    }

    public IEnumerator Indestructable()
    {
        Debug.Log("Indestructable: on");

        yield return new WaitForSeconds(10);

        Debug.Log("Indestructable: off");
    }


   


}