using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HoverLeiste : MonoBehaviour {
    //änderung für später: wenn die vorwärtsbewegung seeeehr schnell werden soll muss der speed nach links und rechts ebenfalls erhöt werden!!!!
    
    //Hover deffintionen
    public float maxHover = 100.0f;
    public float aktuellHover = 0.0f;
    public Image hoverLeiste;

    public CharakterSteuerung CharakterScript;   //ermöglicht den Zugriff auf die Bool im anderen C#

    // Use this for initialization
    void Start () {
        aktuellHover = maxHover;
    }

    // Update is called once per frame
    void Update () {

          if (aktuellHover <= 0)
          {
            CharakterScript.inputHover = false;
            CharakterScript.gravity = 10;
          }
          
    }

    private void FixedUpdate()
    {
        //hoverleisten verbrauch
         if (CharakterScript.inputHover == true)
          {
              aktuellHover = aktuellHover - 0.1f;
              hoverLeiste.fillAmount = aktuellHover / maxHover;
          } 
    }
}
