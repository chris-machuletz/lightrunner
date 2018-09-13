using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    private Transform lookAt;
    private Vector3 startOffset;
    private Vector3 moveVector;
    private float lumenCounter;

    public bool test;
    //Camera Movement at the beginning
    private float transition = 0.0f;
    private float animationDuration = 2.0f; //Dauer der Kamera-Animation am Spielstart
    private Vector3 animationOffset = new Vector3(0, 5, 3);

    // Use this for initialization
    void Start () {
        lookAt = GameObject.FindGameObjectWithTag("Player").transform;
        startOffset = transform.position - lookAt.position;
        test = true;
    }
	
	// Update is called once per frame
	void Update () {

        if (GameObject.Find("Ship") != null) //Es kann nur zugegriffen werden, wenn das Schiff noch nicht zerstört wurde
        {
            moveCamera();
        }

	}

    public void DeathCam()
    {
        if (test)
        {
            transform.Rotate(4.45f, 155.7f, -8.2f);
            
        }
        test = false;
        
    }

   public void moveCamera()
    {
        moveVector = lookAt.position + startOffset;

        //Camera Restriction
        // X
        //moveVector.x = 0;
        // Y
        //moveVector.y = Mathf.Clamp(moveVector.y, 3, 5);

        if (getLumen() <= 0)
        {
            Debug.Log("You Died");
        }
        else
        {
            if (transition > 1.0f)
            {
                transform.position = moveVector;
            }
            else
            {
                // Animation at the Start
                transform.position = Vector3.Lerp(moveVector + animationOffset, moveVector, transition);
                transition += Time.deltaTime * 1 / animationDuration;
                transform.LookAt(lookAt.position + Vector3.up);
            }
        }
    }

    float getLumen()
    {
        return GameObject.Find("Ship").GetComponent<PlayerProps>().lumen;
    }
}
