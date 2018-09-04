using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hauptmenü : MonoBehaviour {

    //Script für die Funktionen des Hauptmenüs
    public Highscore otherscript;   //ermöglicht den Zugriff auf die Bool im anderen C#
    public Text highScore;

    //Start Button
    public void Spielstarten()
    {
        Application.LoadLevel(1);
    }

    //Highscore Button
    public void BesterPunktestand()
    {
        Application.LoadLevel(2);
    }

    //Bringt zum Hauptmenü zurück
    public void Zurück()
    {

        Application.LoadLevel(0);
    }

    //Raumschiff ändern Button
    public void RaumschiffAuswahl()
    {

        Application.LoadLevel(3);
    }

    //Exit Button
    //Dieser Button funktioniert nur wenn das Spiel fertig compiliert ist
    public void Spielbeenden()
    {
        Application.Quit();
    }

    //Highscore Anzeige
    private void Start()
    {
        highScore.text = PlayerPrefs.GetInt("HighScore").ToString();
    }

    public void Reset()     //reset funktion per button des highscores
    {
        PlayerPrefs.DeleteKey("HighScore");
        highScore.text = "0";
    }

    //Hier entsteht die Hover funktion
    public void MausDaruber()
    {

    }
}

//Programmierer Alex
//Quelle: https://www.youtube.com/watch?v=n11oq0Er9h4