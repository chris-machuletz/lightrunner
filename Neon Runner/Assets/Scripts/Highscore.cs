using UnityEngine;
using UnityEngine.UI;

public class Highscore : MonoBehaviour {

    public Text pktzahl;
    public Text highScore;
    public float zähler = 0;
    public float zahl = 0; 

    private void Start()
    {
        highScore.text = PlayerPrefs.GetFloat("HighScore", 0).ToString("F2"); //setzt beim ersten start den highscore auf 0
        PlayerPrefs.DeleteKey("Score");
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
    void Update()
    {
        if (GameObject.Find("Ship").GetComponent<PlayerProps>().isAlive == true)
        {
            zahl += Time.deltaTime;
            pktzahl.text = zahl.ToString("F2");
            PlayerPrefs.SetFloat("Score", zahl);
            zähler = 0;

            if (zahl > PlayerPrefs.GetFloat("HighScore", 0))  //highscore, falls höher wird erhöt
            {
                PlayerPrefs.SetFloat("HighScore", zahl);
                highScore.text = zahl.ToString("F2");
            }
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