using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour {

    public GameObject video;
    public GameObject fakebutton;
    public GameObject richtigebutton;

	// Use this for initialization
	void Start () {
        //StartCoroutine(LoadScene());
        StartCoroutine(Einlauf());
        StartCoroutine(ButtonEinlauf());
    }
	
	// Update is called once per frame
	void Update () {

    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(2.8f);
        SceneManager.LoadScene(1);
    }
    IEnumerator Einlauf()
    {
        yield return new WaitForSeconds(2.8f);
        video.SetActive(false);
    }
    IEnumerator ButtonEinlauf()
    {
        yield return new WaitForSeconds(3.1f);
        richtigebutton.SetActive(true);
        fakebutton.SetActive(false);

    }
}
