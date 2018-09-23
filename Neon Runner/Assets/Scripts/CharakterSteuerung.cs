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
    private float bewegungszähler = 0;

    //gegner / schuss funktion
    private bool abschuss = false;
    public Mesh mesh;
    public Material mats;
    private GameObject schuss;
    private GameObject lightGameObject;
    private float n;

    public GameObject laserPrefab;
    public Transform spawnPoint;
    public GameObject Feuer;

    public bool hover = true;           //darf er hovern?
    public bool inputHover = false;     //wird hovertaste gedrückt
    bool inputJump = false;             //springt er?
    float velocity = 0;                 //links, rechts, gerade aus

    private float animationDuration = 2.0f; // Verhindern, dass das Schiff bewegt wird, wenn die Kamera-Animation läuft (übernommen(nötig??))

    //Gameobjecte für die Schiffe
    public GameObject Schiff1;
    public GameObject Schiff2;
    public GameObject Schiff3;
    public GameObject Schiff4;

    Vector3 moveDirection = Vector3.zero;
    CharacterController characterController;

    public AudioClip schussSound;

    void Start()
    {
        //GUI test = LeftShift;

        characterController = GetComponent<CharacterController>();

        //Fragt die Schiffeinstellungen ab
        switch (PlayerPrefs.GetInt("Schiff", 0))
        {
            case 1:
                Schiff1.SetActive(true);
                break;
            case 2:
                Schiff2.SetActive(true);
                break;
            case 3:
                Schiff3.SetActive(true);
                break;
            case 4:
                Schiff4.SetActive(true);
                break;
            default:
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
        vorwärtsspeed = vorwärtsspeed + 0.005f;    //beschleunigung mit der zeit
        Move();
    }


    void InputCheck()
    {
        velocity = Input.GetAxis("Horizontal") * speed;      //nach links bedeutet - 1 und nach rechts bedeutet +1 so erkennt das prog den unterschied

        if (Input.GetKeyDown(KeyCode.F))
        {

            GetComponent<AudioSource>().PlayOneShot(schussSound);

            GetComponent<MeshFilter>().mesh = mesh = new Mesh();

            schuss = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            schuss.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            schuss.GetComponent<Renderer>().material = mats;
            schuss.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 2);

            schuss.tag = "Schuss";
            SphereCollider kugel = schuss.GetComponent(typeof(SphereCollider)) as SphereCollider;    //aktiviert den collider im cube
            Rigidbody body = schuss.AddComponent<Rigidbody>();  //aktiviert den collider im cube
            body.useGravity = false;
            kugel.isTrigger = true;
            kugel.radius = 1;
            schuss.AddComponent<test>();

            n = this.transform.position.z + 2;
            abschuss = true;
        }
        //!!!!!!!!!!!!!!!!!!!!!
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inputJump = true;
        }
        else
        {
            inputJump = false;
        }

        //hover bei gehaltener shift taste
        if (Input.GetButton("Fire3"))
        {
            if (hover == true)
            {
                gravity = 0;
                inputHover = true;
                moveDirection.y = jumppower;
                moveDirection.y = 0;
            }
            else
            {
                gravity = 10;
            }
        } else
        {
            gravity = 10;
            inputHover = false;
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

        if (velocity == 0)
        {
            bewegungszähler = 0;
            var rotationVector = transform.rotation.eulerAngles;
            rotationVector.x = 0;
            transform.rotation = Quaternion.Euler(rotationVector);
        }
        if (velocity < 0)
        {
                if (bewegungszähler <= 10)  //links
                {
                    this.transform.Rotate(1, 0, 0);
                    bewegungszähler = bewegungszähler + 1;
                }
        }
        if (velocity > 0)
        {
                if (bewegungszähler <= 10)  //rechts
                {
                    this.transform.Rotate(-1, 0, 0);
                    bewegungszähler = bewegungszähler + 1;
                } 
        }

        moveDirection.x = velocity;
        moveDirection.y -= gravity * Time.deltaTime; //erzeugt eine Gravitation für den Charakter
        characterController.Move(moveDirection * Time.deltaTime);   //deltaTime damit die bewegungen nicht von PC unterschiedlich sind
    }
}

//Programmierer Alex