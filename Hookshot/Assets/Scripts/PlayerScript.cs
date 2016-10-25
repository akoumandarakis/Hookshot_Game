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
	private Transform weapon;
    private Vector3 ZeroVector = new Vector3(0, 0, 0);

    void Start()
    {
        Controller = gameObject.GetComponent<CharacterController2D>();
        mainCamera.GetComponent<CameraFollow2D>().startCameraFollow(this.gameObject);
		Animator = gameObject.GetComponent<AnimationController2D> ();
		weapon = this.gameObject.transform.GetChild (1);
    }

    void Update()
    {
        Vector3 velocity = Controller.velocity;

        velocity.x = 0;

		if (Input.GetAxis ("Horizontal") < 0) {
			
			velocity.x = walkSpeed * -1;

			if (Controller.isGrounded) {
				Animator.setAnimation ("WalkAnimation");

				if ((weapon.transform.eulerAngles.z < 90 && weapon.transform.eulerAngles.z >= 0) || (weapon.transform.eulerAngles.z <= 360 && weapon.transform.eulerAngles.z > 270))
				{
					Animator.setFacing ("Right");
					weapon.localScale = new Vector3(1, 1, weapon.localScale.z);
					weapon.position = new Vector3(this.gameObject.transform.position.x + 0.04f, this.gameObject.transform.position.y, this.gameObject.transform.position.z);

				}
				else
				{
					Animator.setFacing("Left");
					weapon.localScale = new Vector3(-1, -1, weapon.localScale.z);
					weapon.position = new Vector3(this.gameObject.transform.position.x - 0.04f, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
				}
			}

			//Animator.setFacing ("Left");

		} 
		else if (Input.GetAxis ("Horizontal") > 0) {
			
			velocity.x = walkSpeed;
			if (Controller.isGrounded) {
				Animator.setAnimation ("WalkAnimation");

				if ((weapon.transform.eulerAngles.z < 90 && weapon.transform.eulerAngles.z >= 0) || (weapon.transform.eulerAngles.z <= 360 && weapon.transform.eulerAngles.z > 270))
				{
					Animator.setFacing ("Right");
					weapon.localScale = new Vector3(1, 1, weapon.localScale.z);
					weapon.position = new Vector3(this.gameObject.transform.position.x + 0.04f, this.gameObject.transform.position.y, this.gameObject.transform.position.z);

				}
				else
				{
					Animator.setFacing("Left");
					weapon.localScale = new Vector3(-1, -1, weapon.localScale.z);
					weapon.position = new Vector3(this.gameObject.transform.position.x - 0.04f, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
				}
			}
				


		} 
		else {
			Animator.setAnimation ("NewIdle");

			if ((weapon.transform.eulerAngles.z < 90 && weapon.transform.eulerAngles.z >= 0) || (weapon.transform.eulerAngles.z <= 360 && weapon.transform.eulerAngles.z > 270))
			{
				Animator.setFacing ("Right");
				weapon.localScale = new Vector3(1, 1, weapon.localScale.z);
				weapon.position = new Vector3(this.gameObject.transform.position.x - 0.045f, this.gameObject.transform.position.y + 0.028f, this.gameObject.transform.position.z);

			}
			else
			{
				Animator.setFacing("Left");
				weapon.localScale = new Vector3(-1, -1, weapon.localScale.z);
				weapon.position = new Vector3(this.gameObject.transform.position.x + 0.045f, this.gameObject.transform.position.y + 0.028f, this.gameObject.transform.position.z);
			}
		}

        if (Input.GetKeyDown("space") && Controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(5f * jumpHeight * -gravity);
			Animator.setAnimation ("NewIdle");

			if ((weapon.transform.eulerAngles.z < 90 && weapon.transform.eulerAngles.z >= 0) || (weapon.transform.eulerAngles.z <= 360 && weapon.transform.eulerAngles.z > 270))
			{
				Animator.setFacing ("Right");
				weapon.localScale = new Vector3(1, 1, weapon.localScale.z);
				weapon.position = new Vector3(this.gameObject.transform.position.x -0.045f, this.gameObject.transform.position.y + 0.028f, this.gameObject.transform.position.z);

			}
			else
			{
				Animator.setFacing("Left");
				weapon.localScale = new Vector3(-1, -1, weapon.localScale.z);
				weapon.position = new Vector3(this.gameObject.transform.position.x + 0.045f, this.gameObject.transform.position.y + 0.028f, this.gameObject.transform.position.z);
			}
        }



        velocity += hookshotAdjust;

        velocity.y += gravity * Time.deltaTime;

        Controller.move(velocity * Time.deltaTime);
    }
}
