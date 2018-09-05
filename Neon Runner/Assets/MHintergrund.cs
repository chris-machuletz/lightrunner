using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MHintergrund : MonoBehaviour {

    public float vorwärtsspeed = 50.0f; //z
    public float speed = 100;    //x
    public float gravity = 10;
    public float jumppower = 10; //sprunghöhe
    public float test = 0;


    bool hover = true;
    public bool inputHover = false;
    bool inputJump = false;
    float velocity = 0;
    Vector3 moveDirection = Vector3.zero;
    CharacterController characterController;

    // Use this for initialization
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Move()
    {
        moveDirection.z = vorwärtsspeed;
        characterController.Move(moveDirection * Time.deltaTime);

        moveDirection.x = velocity;
        moveDirection.y -= gravity * Time.deltaTime; //erzeugt eine Gravitation für den Charakter
        characterController.Move(moveDirection * Time.deltaTime);   //deltaTime damit die bewegungen nicht von PC unterschiedlich sind
    }

    void FixedUpdate()
    {
        vorwärtsspeed = vorwärtsspeed + 0.01f;    //beschleunigung mit der zeit
        Move();
    }
}
