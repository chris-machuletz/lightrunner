using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonSounds : MonoBehaviour {

    public AudioClip music;
    public AudioClip music2;
    private Button button { get { return GetComponent<Button>(); } }
    public AudioSource quelle { get { return GetComponent<AudioSource>(); } }

    // Use this for initialization
    void Start () {
        gameObject.AddComponent<AudioSource>();
        quelle.clip = music;
        quelle.playOnAwake = false;
    }

    public void MausDarüber()
    {
        quelle.PlayOneShot(music);
        //    Image test = this.GetComponent(typeof(Image)) as Image;
        //     test.fillCenter = true;
    }

    public void drücken()
    {
        quelle.PlayOneShot(music2);
    }
    // Update is called once per frame
    void Update () {
		
	}
}
