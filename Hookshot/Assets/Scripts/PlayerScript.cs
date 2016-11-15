using UnityEngine;
using System.Collections;
using System;
using Prime31;

public class PlayerScript : MonoBehaviour {

    public GameObject mainCamera;
    public GameObject backgroundScripts;

    //movement settings
    public float walkSpeed;
    public float jumpHeight;
    public float gravity;

    //hookshot momentum settings
    public float hookMomentumRetardantX;
    public float hookMomentumRetardantY;
    public float hookMomentumThreashhold;

    //hookshot retraction settings (handled by hookshot script, don't set)
    [HideInInspector]
    public Vector3 hookshotAdjust;
    private Vector3 previousHookshotAdjust;
    [HideInInspector]
    public bool hookLatched;
    [HideInInspector]
    public float hookRetractSpeed;

    private CharacterController2D Controller;
	private AnimationController2D Animator;
	private Transform weapon;
    private Vector3 ZeroVector = new Vector3(0, 0, 0);
    private bool hasMomentum;

    void Start()
    {
        Controller = gameObject.GetComponent<CharacterController2D>();
        mainCamera.GetComponent<CameraFollow2D>().startCameraFollow(this.gameObject);
		Animator = gameObject.GetComponent<AnimationController2D> ();
		weapon = this.gameObject.transform.GetChild (1);
        hasMomentum = false;
    }

    void Update()
    {
        //if you are currently latched, retract the player to the hook
        if (hookLatched)
        {
            HookMovement();
        }
        //if not latched but the player has residual momentum from the hook, handle that
        else if (hasMomentum)
        {
            HookMomentumHandler();
        }

        //always take player input
        StandardMovement();
    }

    private void HookMovement()
    {
        previousHookshotAdjust = hookshotAdjust;
        hasMomentum = true;
        Controller.move(hookshotAdjust.normalized * Time.deltaTime * hookRetractSpeed);
    }

    private void HookMomentumHandler()
    {
        hasMomentum = false;
    }

    private void StandardMovement()
    {
        Vector3 velocity = Controller.velocity;

        velocity.x = 0;

        if (Input.GetAxis("Horizontal") < 0)
        {
            velocity.x = walkSpeed * -1;

            if (Controller.isGrounded)
            {
                Animator.setAnimation("WalkAnimation");

                if ((weapon.transform.eulerAngles.z < 90 && weapon.transform.eulerAngles.z >= 0) || (weapon.transform.eulerAngles.z <= 360 && weapon.transform.eulerAngles.z > 270))
                {
                    Animator.setFacing("Right");
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
        else if (Input.GetAxis("Horizontal") > 0)
        {
            velocity.x = walkSpeed;

            if (Controller.isGrounded)
            {
                Animator.setAnimation("WalkAnimation");

                if ((weapon.transform.eulerAngles.z < 90 && weapon.transform.eulerAngles.z >= 0) || (weapon.transform.eulerAngles.z <= 360 && weapon.transform.eulerAngles.z > 270))
                {
                    Animator.setFacing("Right");
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
        else
        {
            Animator.setAnimation("NewIdle");

            if ((weapon.transform.eulerAngles.z < 90 && weapon.transform.eulerAngles.z >= 0) || (weapon.transform.eulerAngles.z <= 360 && weapon.transform.eulerAngles.z > 270))
            {
                Animator.setFacing("Right");
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
			velocity.y = Jump (velocity);
        }

		if (Input.GetAxis ("Vertical") < 0 && hookLatched) 
		{
			velocity.y = walkSpeed * -0.5f;
		}

		if (Input.GetAxis ("Vertical") > 0 && hookLatched) 
		{
			velocity.y = walkSpeed * 0.5f;
		}

        if (hookLatched)
        {
            HookMovement();
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        Controller.move(velocity * Time.deltaTime);
    }

    public void OnDestroy()
    {
        //play death animation
        transform.parent.gameObject.AddComponent<DeathMenuScript>();

    }

	public float Jump(Vector3 velocity)
	{
		velocity.y = Mathf.Sqrt(5f * jumpHeight * -gravity);
		Animator.setAnimation("NewIdle");

		if ((weapon.transform.eulerAngles.z < 90 && weapon.transform.eulerAngles.z >= 0) || (weapon.transform.eulerAngles.z <= 360 && weapon.transform.eulerAngles.z > 270))
		{
			Animator.setFacing("Right");
			weapon.localScale = new Vector3(1, 1, weapon.localScale.z);
			weapon.position = new Vector3(this.gameObject.transform.position.x - 0.045f, this.gameObject.transform.position.y + 0.028f, this.gameObject.transform.position.z);
		}
		else
		{
			Animator.setFacing("Left");
			weapon.localScale = new Vector3(-1, -1, weapon.localScale.z);
			weapon.position = new Vector3(this.gameObject.transform.position.x + 0.045f, this.gameObject.transform.position.y + 0.028f, this.gameObject.transform.position.z);
		}
		return velocity.y;
	}

	public string GetDirectionAiming()
	{
		if ((weapon.transform.eulerAngles.z < 90 && weapon.transform.eulerAngles.z >= 0) || (weapon.transform.eulerAngles.z <= 360 && weapon.transform.eulerAngles.z > 270)) {
			return "Right";
		} else {
			return "Left";
		} 
	}
}
