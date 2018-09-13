using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonSounds : MonoBehaviour {

    public AudioClip music;
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
    }
}
