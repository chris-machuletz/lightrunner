using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lumen : MonoBehaviour {

    public float lumenCount;
    public Text lumenCountText;
    public Text deathText;

    // Use this for initialization
    void Start () {
        lumenCount = 50.0f;
        SetCountText();
        deathText.text = "";
    }
	
	// Update is called once per frame
	void Update () {
        lumenCount -= Time.deltaTime * 10.0f;
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
        if (collision.gameObject.name == "LumenCube")
        {
            Destroy(collision.gameObject);
            GameObject.Find("Ship").GetComponent<Lumen>().lumenCount += 1000;
        }
    }
}
