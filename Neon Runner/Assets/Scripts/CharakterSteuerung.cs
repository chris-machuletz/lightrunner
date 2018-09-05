using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharakterSteuerung : MonoBehaviour
{
    //änderung für später: wenn die vorwärtsbewegung seeeehr schnell werden soll muss der speed nach links und rechts ebenfalls erhöt werden!!!!


    public float vorwärtsspeed = 50.0f; //z
    public float speed = 100;    //x
    public float gravity = 10;
    public float jumppower = 10; //sprunghöhe
    public float test = 0;


    bool hover = true;
    public bool inputHover = false;
    bool inputJump = false;
    float velocity = 0;

    private float animationDuration = 2.0f; // Verhindern, dass das Schiff bewegt wird, wenn die Kamera-Animation läuft (übernommen(nötig??))

    //Gameobjecte für die Schiffe
    public GameObject Schiff1;
    public GameObject Schiff2;
    public GameObject Schiff3;
    public GameObject Schiff4;
    public GameObject Schiff5;

    Vector3 moveDirection = Vector3.zero;
    CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        //Fragt die Schiffeinstellungen ab
        switch (PlayerPrefs.GetInt("Schiff", 0))
        {
            case 1:
                print("Schiff 1 ausgewählt");
                Schiff1.SetActive(true);
                break;
            case 2:
                print("Schiff 2 ausgewählt");
                Schiff2.SetActive(true);
                break;
            case 3:
                print("Schiff 3 ausgewählt");
                Schiff3.SetActive(true);
                break;
            case 4:
                print("Schiff 4 ausgewählt");
                Schiff4.SetActive(true);
                break;
            case 5:
                print("Schiff 5 ausgewählt");
                Schiff5.SetActive(true);
                break;
            default:
                print("Kein Schiff ausgewählt");
                Schiff1.SetActive(true);
                break;
        }
    }

    void Update()
    {
        if (Time.time < animationDuration) // Verhindern, dass das Schiff bewegt wird, wenn die Kamera-Animation läuft (übernommen(nötig??))
        {
            characterController.Move(Vector3.forward * Time.deltaTime * velocity);
            return;
        }
    }

    void FixedUpdate()
    {
        InputCheck();
        vorwärtsspeed = vorwärtsspeed + 0.01f;    //beschleunigung mit der zeit
        Move();
    }

    void InputCheck()
    {
        velocity = Input.GetAxis("Horizontal") * speed;      //nach links bedeutet - 1 und nach rechts bedeutet +1 so erkennt das prog den unterschied

        if (Input.GetKeyDown(KeyCode.Space))
        {
            inputJump = true;
        }
        else
        {
            inputJump = false;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            if (inputHover == false)
            {
                gravity = 0;
                inputHover = true;
                moveDirection.y = jumppower;
                moveDirection.y = 0;
            }
            else
            {
                gravity = 10;
                inputHover = false;
            }
        }
    }

    void Move()
    {
        moveDirection.z = vorwärtsspeed;
        characterController.Move(moveDirection * Time.deltaTime);

        if (characterController.isGrounded) //if jump muss vor gravity sein, weil es danach gravity - bekommt
        {
            if (inputJump)
            {
                moveDirection.y = jumppower;
            }
        }

        moveDirection.x = velocity;
        moveDirection.y -= gravity * Time.deltaTime; //erzeugt eine Gravitation für den Charakter
        characterController.Move(moveDirection * Time.deltaTime);   //deltaTime damit die bewegungen nicht von PC unterschiedlich sind
    }



    //ohne funktion, vllt noch nötig??
    void SetAnimation()
    {

    }
}

//Programmierer Alex