using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Hauptmenü : MonoBehaviour {

    //warten
    float waitT = 0.45f; //wartezeit zwischen dem umschalten

    //Script für die Funktionen des Hauptmenüs
    public Highscore otherscript;   //ermöglicht den Zugriff auf die Bool im anderen C#
    public Text highScore;
    public Text score;

    //drücksound
    public AudioClip music;
    public AudioSource quelle { get { return GetComponent<AudioSource>(); } }

    //Start Button
    public void Spielstarten()
    {
        StartCoroutine("Map1");
    }

    //Highscore Button
    public void BesterPunktestand()
    {
        StartCoroutine("Map2");
    }

    //Bringt zum Hauptmenü zurück
    public void Zurück()
    {
        StartCoroutine("Map3");
    }

    //Raumschiff ändern Button
    public void RaumschiffAuswahl()
    {
        StartCoroutine("Map4");
    }

    //Exit Button
    //Dieser Button funktioniert nur wenn das Spiel fertig compiliert ist
    public void Spielbeenden()
    {
        StartCoroutine("Map5");
    }

    //Highscore Anzeige
    private void Start()
    {
      if (highScore != null)
        {
            highScore.text = PlayerPrefs.GetInt("HighScore").ToString();
        }
      if (score != null)
        {
            score.text = PlayerPrefs.GetInt("Score").ToString();
        }

        gameObject.AddComponent<AudioSource>();
        quelle.clip = music;
        quelle.playOnAwake = false;
    }

    public void Reset()     //reset funktion per button des highscores
    {
        StartCoroutine("Map6");
    }
    //aufrufe mit warteschleifen, damit man den ton hört und er nicht unterbrochen wird

    IEnumerator Map1() 
    {
        quelle.PlayOneShot(music);
        yield return new WaitForSeconds(waitT); //wartet 
        SceneManager.LoadScene(1);
    }
    IEnumerator Map2()
    {
        quelle.PlayOneShot(music);
        yield return new WaitForSeconds(waitT); //wartet 
        Application.LoadLevel(2);
    }
    IEnumerator Map3()
    {
        quelle.PlayOneShot(music);
        yield return new WaitForSeconds(waitT); //wartet 
        Application.LoadLevel(0);
    }
    IEnumerator Map4() 
    {
        quelle.PlayOneShot(music);
        yield return new WaitForSeconds(waitT); //wartet 
        Application.LoadLevel(3);
    }
    IEnumerator Map5()
    {
        quelle.PlayOneShot(music);
        yield return new WaitForSeconds(waitT); //wartet 
        Application.Quit();
    }
    IEnumerator Map6()
    {
        quelle.PlayOneShot(music);
        yield return new WaitForSeconds(waitT); //wartet 
        PlayerPrefs.DeleteKey("HighScore");
        highScore.text = "0";
    }
}

//Programmierer Alex
//Quelle: https://www.youtube.com/watch?v=n11oq0Er9h4