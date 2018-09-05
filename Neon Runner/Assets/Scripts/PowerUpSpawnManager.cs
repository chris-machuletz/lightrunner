using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawnManager : MonoBehaviour
{
    int counter = 1;
    private void Update()
    {
        if(counter %20 == 0 )
        {
            //Raffle();
        }
        counter++;
    }

    void getALife() // Fügt dem Spieler ein Extraleben hinzu, sofern er nicht bereits 3 besitzt
    {
        if (GameObject.Find("Ship").GetComponent<PlayerProps>().lifes <= 3)
        {
            GameObject.Find("Ship").GetComponent<PlayerProps>().lifes++;
            Debug.Log(GameObject.Find("Ship").GetComponent<PlayerProps>().lifes);
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

   


}