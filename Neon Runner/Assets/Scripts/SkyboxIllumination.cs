using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxIllumination : MonoBehaviour {

	// Use this for initialization
	void Start () {
        RenderSettings.skybox.SetFloat("_Exposure", 1);
        RenderSettings.skybox.SetColor("_Tint", Color.red);
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(GameObject.Find("Ship").GetComponent<PlayerProps>().lumen / 125);
        //if ((GameObject.Find("Ship").GetComponent<PlayerProps>().lumen / 125) <= 3)
        //{
        //    RenderSettings.skybox.SetFloat("_Exposure", (GameObject.Find("Ship").GetComponent<PlayerProps>().lumen / 125));
        //}
	}
}
