using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lumen : MonoBehaviour {

    public float lumenCount;
    public Text lumenCountText;
    public Text deathText;

    private float vel; //geschwindigkeit des Spielers (aus Shipmovement.cs)

    // Use this for initialization
    void Start () {
        vel = GameObject.Find("Ship").GetComponent<ShipMovement>().velocity;
        lumenCount = 15000.0f;
        SetCountText();
        deathText.text = "";
    }
	
	// Update is called once per frame
	void Update () {
        lumenCount -= Time.deltaTime * (vel / 10);
        SetCountText();
        if (lumenCount <= 0)
        {
            deathText.text = "YOU DIED. Score: 0";
        }
	}

    void SetCountText()
    {
        int lumenCountInt = (int)lumenCount;
        lumenCountText.text = "Lumen: " + lumenCountInt.ToString();
    }

    private void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.name == "LumenCube" || collision.gameObject.name == "LumenCube(Clone)")
        {
            Destroy(collision.gameObject);
            GameObject.Find("Ship").GetComponent<Lumen>().lumenCount += Random.Range(5,10);
            AudioSource source = GetComponent<AudioSource>();
            source.Play();
        }
    }
}
