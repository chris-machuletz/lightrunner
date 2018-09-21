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

        shipli.transform.position = gameObject.transform.position;

        //gameObject.GetComponent<shiplight>();


        //if (gameObject.name == "ship01")
        //{
        //    shiplight.transform.position = gameObject.transform.position;
        //}

        //if (gameObject.name == "ship01_neonframe")
        //{
        //    shiplight.transform.position = gameObject.transform.position;
        //}

        //if (gameObject.name == "ship02")
        //{
        //    shiplight.transform.position = gameObject.transform.position;
        //}

        //if (gameObject.name == "ship03")
        //{
        //    shiplight.transform.position = vec;
        //    vec.y += 3;
        //    shiplight.transform.position = vec;
        //}
    }


    // Update is called once per frame
    void Update()
    {
        GameObject.Find("shipli").transform.position = gameObject.transform.position;
    }
}
