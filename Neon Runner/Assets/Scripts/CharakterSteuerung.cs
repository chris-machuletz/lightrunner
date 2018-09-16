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

    //schwenkbewegungen 
    int schwenkung = 0;
    int schwenkende = 3;

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
   // public GameObject Schiff5;

    Vector3 moveDirection = Vector3.zero;
    CharacterController characterController;

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
          //  case 5:
            //    Schiff5.SetActive(true);
            //    break;
            default:
                Schiff1.SetActive(true);
                break;
        }

    }

    void Update()
    {

      /*  if (abschuss == true)
        {
            //Lösung    GameObject.FindGameObjectWithTag("Schuss").transform.position = new Vector3(GameObject.FindGameObjectWithTag("Schuss").transform.position.x, GameObject.FindGameObjectWithTag("Schuss").transform.position.y, n);
            GameObject.FindGameObjectWithTag("Schuss").transform.position = new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x, GameObject.FindGameObjectWithTag("Player").transform.position.y, n);
            n = n + 4;
            lightGameObject.transform.position = schuss.transform.position;
            if (schuss.transform.position.z >= GameObject.FindGameObjectWithTag("Player").transform.position.z + 100)
            {
                Destroy(GameObject.FindWithTag("Schuss"));
                Destroy(GameObject.FindWithTag("SchussLicht"));
                abschuss = false;
            }
        } */
     /*   if (schuss != null)
        {
            if (schuss.transform.position.z >= GameObject.FindGameObjectWithTag("Player").transform.position.z + 100)
            {
                Destroy(GameObject.FindWithTag("Schuss"));
                Destroy(GameObject.FindWithTag("SchussLicht"));
                abschuss = false;
            }
        } */

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
        
        //besoffener code!!!!
        if (Input.GetKeyDown(KeyCode.F))
        {
         /*   Feuer = Instantiate(laserPrefab, spawnPoint.position + new Vector3(0,0,10), Quaternion.identity);
            Feuer.tag = "Schuss";
            abschuss = true;
            n = this.transform.position.z + 1;
            //laser.transform.position = */

            
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
            test aa = schuss.AddComponent<test>();

            //Leuchten des Cubes 
            /*lightGameObject = new GameObject("Schusslicht");
            lightGameObject.tag = "SchussLicht";
            Light lightComp = lightGameObject.AddComponent<Light>();
            lightComp.color = Color.red;
            lightComp.intensity = 8.5f;
            lightComp.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
            lightGameObject.transform.position = schuss.transform.position;
            */
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

        // if (Input.GetButton("Jump")) wenn eine taste gedrückt gehalten wird 

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
                //inputHover = false;
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

        //schwenk bewegung des schiffes
        //alles auskommentierte war der verusch, die rückschwenk bewegung des schiffes auch in einer flüssigen bewegung zu machen
        //hat aber zu rotations bugs geführt
        if (velocity == 0)
        { /*
            if (schwenkende == 1)
            {
                bewegungszähler = 0;
                schwenkende = 2;
            }
            if (schwenkung == 1)        //schwenkung = 1 bedeutet links, schwenkung = 2 bedeutet rechts
            {
                for (int i = 0; i <= 10; i++)
                {
                    this.transform.Rotate(-1, 0, 0);
                    bewegungszähler = bewegungszähler + 1;
                }
               /* if (bewegungszähler <= 10)  //links
                {
                    this.transform.Rotate(-1, 0, 0);
                    bewegungszähler = bewegungszähler + 1;
                    
                } else {
                    bewegungszähler = 0;
                    schwenkung = 3;
                    schwenkende = 3;
                } 
                bewegungszähler = 0;
                schwenkung = 3;
                schwenkende = 3;
            }
            if (schwenkung == 2)        //schwenkung = 1 bedeutet links, schwenkung = 2 bedeutet rechts
            {
                if (bewegungszähler <= 10)  //links
                {
                    this.transform.Rotate(1, 0, 0);
                    bewegungszähler = bewegungszähler + 1;
                }
                else
                {
                    bewegungszähler = 0;
                    schwenkung = 3;
                    schwenkende = 3;
                }
            } */
            bewegungszähler = 0;
            var rotationVector = transform.rotation.eulerAngles;
            rotationVector.x = 0;
            transform.rotation = Quaternion.Euler(rotationVector);
        }
        if (velocity < 0)
        {
           /* if (schwenkende == 3)
            {*/
                if (bewegungszähler <= 10)  //links
                {
                    this.transform.Rotate(1, 0, 0);
                    bewegungszähler = bewegungszähler + 1;
                }/* else
                {
                    schwenkung = 1;
                    schwenkende = 1;
                }
            }*/
        }
        if (velocity > 0)
        {
            /*if (schwenkende == 3)
            {*/
                if (bewegungszähler <= 10)  //rechts
                {
                    this.transform.Rotate(-1, 0, 0);
                    bewegungszähler = bewegungszähler + 1;
                } /*else
                {
                    schwenkung = 2;
                    schwenkende = 1;
                }*/
           // }
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