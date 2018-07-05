using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour {

    private CharacterController controller;
    private Vector3 moveVector;

    private float velocity = 10.0f;
    private float gravity = 10.0f;
    private float verticalVelocity = 0.0f;

    private float animationDuration = 2.0f; // Verhindern, dass das Schiff bewegt wird, wenn die Kamera-Animation läuft

	// Use this for initialization
	void Start () {
        this.transform.Translate(0, 0.5f, 0);
        controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Time.time < animationDuration) // Verhindern, dass das Schiff bewegt wird, wenn die Kamera-Animation läuft
        {
            controller.Move(Vector3.forward * Time.deltaTime * velocity);
            return;
        }

        moveVector = Vector3.zero;

        if (controller.isGrounded)
        {
            verticalVelocity = -0.2f;
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        // X-Movement
        moveVector.x = Input.GetAxisRaw("Horizontal") * velocity * 2;

        // Y-Movement
        moveVector.y = verticalVelocity;


        // Z-Movement
        moveVector.z = velocity;

        controller.Move(moveVector * Time.deltaTime);
	}
}
