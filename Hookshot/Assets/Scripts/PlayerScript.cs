using UnityEngine;
using System.Collections;
using System;
using Prime31;

public class PlayerScript : MonoBehaviour {

    public GameObject mainCamera;

    public float walkSpeed;
    public float jumpHeight;
    public float gravity;

    [HideInInspector]
    public Vector3 hookshotAdjust;
    [HideInInspector]
    public bool hookLatched;

    private CharacterController2D Controller;
	private AnimationController2D Animator;
    private Vector3 ZeroVector = new Vector3(0, 0, 0);

    void Start()
    {
        Controller = gameObject.GetComponent<CharacterController2D>();
        mainCamera.GetComponent<CameraFollow2D>().startCameraFollow(this.gameObject);
		Animator = gameObject.GetComponent<AnimationController2D> ();
    }

    void Update()
    {
        Vector3 velocity = Controller.velocity;

        velocity.x = 0;

		if (Input.GetAxis ("Horizontal") < 0) {
			
			velocity.x = walkSpeed * -1;

			if (Controller.isGrounded) {
				Animator.setAnimation ("WalkAnimation");
			}

			//Animator.setFacing ("Left");

		} 
		else if (Input.GetAxis ("Horizontal") > 0) {
			
			velocity.x = walkSpeed;
			if (Controller.isGrounded) {
				Animator.setAnimation ("WalkAnimation");
			}

			//Animator.setFacing ("Right");

		} 
		else {
			Animator.setAnimation ("PlayerIdle");
		}

        if (Input.GetKeyDown("space") && Controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(5f * jumpHeight * -gravity);
			Animator.setAnimation ("PlayerIdle");
        }

        velocity += hookshotAdjust;

        velocity.y += gravity * Time.deltaTime;

        Controller.move(velocity * Time.deltaTime);
    }
}
