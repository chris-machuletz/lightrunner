using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hauptmenü : MonoBehaviour {

    //Script für die Funktionen des Hauptmenüs

    //Start Button
    public void Spielstarten()
    {
        Application.LoadLevel(0);
    }

    //Highscore Button
    public void BesterPunktestand()
    {
        Application.LoadLevel(2);
    }

    //Exit Button
    //Dieser Button funktioniert nur wenn das Spiel fertig compiliert ist
    public void Spielbeenden()
    {
        Application.Quit();
    }
}

//Quelle: https://www.youtube.com/watch?v=n11oq0Er9h4