using UnityEngine;
using System.Collections;
using System;
using Prime31;

public class PlayerScript : MonoBehaviour {

    public GameObject mainCamera;

    public float walkSpeed;
    public float jumpHeight;
    public float gravity;

    public Vector3 hookshotAdjust;

    private CharacterController2D Controller;

    void Start()
    {
        Controller = gameObject.GetComponent<CharacterController2D>();
        mainCamera.GetComponent<CameraFollow2D>().startCameraFollow(this.gameObject);
        hookshotAdjust = new Vector3(0, 0, 0);
    }

    void Update()
    {
        Vector3 velocity = Controller.velocity;

        velocity.x = 0;

        if (Input.GetAxis("Horizontal") < 0)
        {
            velocity.x = walkSpeed * -1;
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            velocity.x = walkSpeed;
        }

        if (Input.GetKeyDown("space") && Controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(5f * jumpHeight * -gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        Controller.move(velocity * Time.deltaTime);
    }
}
