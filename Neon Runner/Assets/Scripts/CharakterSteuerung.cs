using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharakterSteuerung : MonoBehaviour
{
    //änderung für später: wenn die vorwärtsbewegung seeeehr schnell werden soll muss der speed nach links und rechts ebenfalls erhöt werden!!!!


    public float vorwärtsspeed = 50.0f; //z
    public float speed = 100;    //x
    public float gravity = 10;
    public float jumppower = 5; //sprunghöhe
    public float test = 0;


    bool hover = true;
    public bool inputHover = false;
    bool inputJump = false;
    float velocity = 0;

    private float animationDuration = 2.0f; // Verhindern, dass das Schiff bewegt wird, wenn die Kamera-Animation läuft (übernommen(nötig??))

    //test charakter änderungen
    public MeshFilter mesh;
    public Mesh mesh1;
    public GameObject selectedShip;
    public GameObject empty;
    //

    Vector3 moveDirection = Vector3.zero;
    CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        //test bereich von den charakter änderungen 

        empty = GameObject.FindGameObjectWithTag("Player");

        selectedShip = GameObject.Find("ship01");

        empty = selectedShip;
        // = GameObject.Find("ship01");

        //selectedShip.transform.position = gameObject.transform.position;
        //empty = selectedShip;
        //gameObject.GetComponent = GameObject.Find("ship01");
        /*
        if (1 == PlayerPrefs.GetInt("Schiff", 0))
        {
            selectedShip = GameObject.Find("ship01");
        }
        */

        /* mesh = GameObject.Find("Ship").GetComponent<MeshFilter>();

         //mesh1 = GameObject.Find("ship01").GetComponent<MeshFilter>().mesh;


         transform.Rotate(new Vector3(-90, -90, 0));
         transform.localScale = new Vector3(20f, 20f, 20f);
         transform.position = new Vector3(0, 0f, 0);

         mesh.mesh = mesh1;*/
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
        selectedShip = GameObject.Find("ship01");
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