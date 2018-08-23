using UnityEngine;
using UnityEngine.UI;

public class Highscore : MonoBehaviour {

    public Text pktzahl;
    public Text highScore;
    public float zähler = 0;
    public int zahl = 0; 

    private void Start()
    {
        highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString(); //setzt beim ersten start den highscore auf 0
    }

    /*  zählt pkt pekr knopf druck bsp code nur!!
    public void RollDice () //bei jedem klick eine zufalls zahl
    {
        int number = Random.Range(1, 7);
        pktzahl.text = number.ToString();

        if (number > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", number);
            highScore.text = number.ToString();
        }
    }
    */
    void FixedUpdate()
    {
        if (zähler >= 1)    //zeitverzögerung zählt die pkt
        {
            zahl = zahl + 1;
            pktzahl.text = zahl.ToString();
            zähler = 0;
        } else
        {
            zähler = zähler + 0.05f;    //beschleunigung
        }

        if (zahl > PlayerPrefs.GetInt("HighScore", 0))  //highscore, falls höher wird erhöt
        {
            PlayerPrefs.SetInt("HighScore", zahl);
            highScore.text = zahl.ToString();
        }
    }

    public void Reset()     //reset funktion per button des highscores
    {
        PlayerPrefs.DeleteKey("HighScore");
        highScore.text = "0";
    }

}

//Programmierer Alex
//Quellen: https://www.youtube.com/watch?v=vZU51tbgMXk