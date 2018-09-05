using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lumen : MonoBehaviour {

    public Text lumenCountText;
    public Text deathText;

    private float vel; //geschwindigkeit des Spielers (aus Shipmovement.cs)

    // Use this for initialization
    void Start () {
        vel = GameObject.Find("Ship").GetComponent<CharakterSteuerung>().vorwärtsspeed;
        SetCountText();
        deathText.text = "";
    }

	// Update is called once per frame
	void Update () {
        GameObject.Find("Ship").GetComponent<PlayerProps>().lumen -= Time.deltaTime * (vel / 10);
        SetCountText();
        if (GameObject.Find("Ship").GetComponent<PlayerProps>().lumen <= 0)
        {
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
            AudioSource source = GetComponent<AudioSource>();
            source.Play();
        }
    }
}
