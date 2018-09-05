using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoverEffektTest : MonoBehaviour {

    //Besitzt noch keine Funktion soll vielleicht dazu benutzt werden beim Hauptmenü bei den Buttons ein Hover Effekt zu erzeugen
	// Use this for initialization
	void Start () {
        Image test = this.GetComponent(typeof(Image)) as Image;
        test.fillCenter = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MausDaruber()
    {

    }
}
