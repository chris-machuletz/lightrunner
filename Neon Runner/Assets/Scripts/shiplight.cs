using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shiplight : MonoBehaviour {


    // Use this for initialization
    public void Start()
    {
        Shiplight();
    }



    void Shiplight()
    {
        GameObject shipli = new GameObject("shipli");

        Light light = shipli.AddComponent<Light>();
        light.color = Color.white;
        light.type = LightType.Spot;
        light.spotAngle = 95;
        light.intensity = 80;
        light.transform.Rotate(0, 0, 0.6f);
        light.renderMode = LightRenderMode.ForcePixel;

        shipli.transform.position = gameObject.transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        GameObject.Find("shipli").transform.position = gameObject.transform.position; //fügt Licht-Objekt der Position des Raumschiffs hinzu
    }
}
