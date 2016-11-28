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
    public float hookMoveResist;

    //hookshot retraction settings (handled by hookshot script, don't set)
    [HideInInspector]
    public Vector3 hookshotAdjust;
    [HideInInspector]
    public bool hookLatched;
    [HideInInspector]
    public float hookRetractSpeed;

    private CharacterController2D Controller;
	private AnimationController2D Animator;
	private GameObject gun;
	private Transform weapon;
	private SpriteRenderer weaponRenderer;
    private Vector3 ZeroVector = new Vector3(0, 0, 0);

	[HideInInspector]
	public bool IsZeroG;
	/// <summary>
	/// Used to determine camera movement
	/// </summary>
	private string currentDirectionAiming = "Right";

	/// <summary>
	/// Used to determine animation
	/// </summary>
	private string directionAiming = "Right";

    void Start()
    {
        Controller = gameObject.GetComponent<CharacterController2D>();
        mainCamera.GetComponent<CameraFollow2D>().startCameraFollow(this.gameObject);
		Animator = gameObject.GetComponent<AnimationController2D> ();
		weapon = this.gameObject.transform.GetChild (1);
		//weapon.GetComponent<SpriteRenderer> ();
		weaponRenderer = this.gameObject.GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        //if you are currently latched, retract the player to the hook
        if (hookLatched)
        {
            HookMovement();
        }

		//always take player input
		if (IsZeroG) 
		{
			ZeroGMovement();
		} 
		else 
		{
			StandardMovement();
		}
    }

    private void HookMovement()
    {
        Controller.move(
            hookshotAdjust.normalized 
            * Time.deltaTime 
            * hookRetractSpeed 
            );
    }

    private void StandardMovement()
    {
        Vector3 velocity = Controller.velocity;

        velocity.x = 0;

		if ((weapon.transform.eulerAngles.z < 90 && weapon.transform.eulerAngles.z >= 0) || (weapon.transform.eulerAngles.z <= 360 && weapon.transform.eulerAngles.z > 270)) {
			directionAiming = "Right";
		} else {
			directionAiming = "Left";
		}

		if (directionAiming == "Right") {
			weapon.localScale = new Vector3 (1, 1, weapon.localScale.z);
		} else {
			weapon.localScale = new Vector3(1, -1, weapon.localScale.z);
		}

		//Moving Left
        if (Input.GetAxis("Horizontal") < 0)
        {
            velocity.x = walkSpeed * -1;

            if (Controller.isGrounded)
            {
				if (directionAiming == "Left") {
					Animator.setAnimation ("Walk_Left");
					weapon.position = new Vector3(this.gameObject.transform.position.x - 0.02f, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
					weaponRenderer.sortingOrder = -1;

				} else {
					Animator.setAnimation("Walk_Right_Reverse");
					weapon.position = new Vector3(this.gameObject.transform.position.x + 0.04f, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
					weaponRenderer.sortingOrder = 1;
				}

			} else {
				if (directionAiming == "Left") {
					Animator.setAnimation ("Jump_Left");
					weapon.position = new Vector3 (this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
					weaponRenderer.sortingOrder = -1;

				} else {
					Animator.setAnimation ("Jump_Right");
					weapon.position = new Vector3 (this.gameObject.transform.position.x + 0.04f, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
					weaponRenderer.sortingOrder = 1;
				}
			}
        }
		//Moving Right
        else if (Input.GetAxis("Horizontal") > 0)
        {
            velocity.x = walkSpeed;

			if (Controller.isGrounded) {
				if (directionAiming == "Left") {
					Animator.setAnimation ("Walk_Left_Reverse");
					weapon.position = new Vector3 (this.gameObject.transform.position.x - 0.02f, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
					weaponRenderer.sortingOrder = -1;

				} else {
					Animator.setAnimation ("Walk_Right");
					weapon.position = new Vector3 (this.gameObject.transform.position.x + 0.04f, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
					weaponRenderer.sortingOrder = 1;
				}
			} else {
				if (directionAiming == "Left") {
					Animator.setAnimation ("Jump_Left");
					weapon.position = new Vector3 (this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
					weaponRenderer.sortingOrder = -1;

				} else {
					Animator.setAnimation ("Jump_Right");
					weapon.position = new Vector3 (this.gameObject.transform.position.x + 0.04f, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
					weaponRenderer.sortingOrder = 1;
				}
			}
        }
		//Standing Still
        else
        {
			if (Controller.isGrounded) {
				if (directionAiming == "Left") {
					Animator.setAnimation("NewIdle");
					weapon.position = new Vector3 (this.gameObject.transform.position.x - 0.045f, this.gameObject.transform.position.y + 0.028f, this.gameObject.transform.position.z);
					weaponRenderer.sortingOrder = -1;

				} else {
					Animator.setAnimation("NewIdle");
					weapon.position = new Vector3 (this.gameObject.transform.position.x - 0.045f, this.gameObject.transform.position.y + 0.028f, this.gameObject.transform.position.z);
					weaponRenderer.sortingOrder = 1;
				}
			} else {
				if (directionAiming == "Left") {
					Animator.setAnimation ("Jump_Left");
					weapon.position = new Vector3 (this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
					weaponRenderer.sortingOrder = -1;

				} else {
					Animator.setAnimation ("Jump_Right");
					weapon.position = new Vector3 (this.gameObject.transform.position.x + 0.04f, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
					weaponRenderer.sortingOrder = 1;
				}
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
            Controller.move(velocity * Time.deltaTime * hookMoveResist);
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
            Controller.move(velocity * Time.deltaTime);
        }

    }

	public void ZeroGMovement()
	{
		Vector3 velocity = Controller.velocity;
		Vector3 acceleration = new Vector3();
		Vector2 direction = new Vector2 (0,0);

		if ((weapon.transform.eulerAngles.z < 90 && weapon.transform.eulerAngles.z >= 0) || (weapon.transform.eulerAngles.z <= 360 && weapon.transform.eulerAngles.z > 270)) {
			directionAiming = "Right";

			Animator.setAnimation ("Jump_Right");
			weapon.position = new Vector3 (this.gameObject.transform.position.x + 0.04f, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
			weaponRenderer.sortingOrder = 1;
		} else {
			directionAiming = "Left";

			Animator.setAnimation ("Jump_Left");
			weapon.position = new Vector3 (this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
			weaponRenderer.sortingOrder = -1;
		}

		if (directionAiming == "Right") {
			weapon.localScale = new Vector3 (1, 1, weapon.localScale.z);
		} else {
			weapon.localScale = new Vector3(1, -1, weapon.localScale.z);
		}

		if (Input.GetAxis("Horizontal") < 0)
		{
			direction = new Vector2 (-1, direction.y); 
		}

		if (Input.GetAxis("Horizontal") > 0)
		{
			direction = new Vector2 (1, direction.y); 
		}

		if (Input.GetAxis ("Vertical") < 0) 
		{
			direction = new Vector2 (direction.x, -1); 
		}

		if (Input.GetAxis ("Vertical") > 0) 
		{
			direction = new Vector2 (direction.x, 1); 
		}

		if (hookLatched)
		{
			HookMovement();
		}
			
		acceleration = new Vector3 (walkSpeed * direction.x, walkSpeed * direction.y);

		acceleration *= Time.deltaTime;

		velocity = new Vector3 (velocity.x + acceleration.x, velocity.y + acceleration.y);

		Controller.move(velocity *= Time.deltaTime);
	}

    public void OnDestroy()
    {
        //play death animation
        transform.parent.gameObject.AddComponent<DeathMenuScript>();

    }

	public float Jump(Vector3 velocity)
	{
		velocity.y = Mathf.Sqrt(5f * jumpHeight * -gravity);

		if (directionAiming == "Left") {
			weapon.position = new Vector3(this.gameObject.transform.position.x - 0.045f, this.gameObject.transform.position.y + 0.028f, this.gameObject.transform.position.z);
			weaponRenderer.sortingOrder = -1;

		} else {
			weapon.position = new Vector3(this.gameObject.transform.position.x - 0.045f, this.gameObject.transform.position.y + 0.028f, this.gameObject.transform.position.z);
			weaponRenderer.sortingOrder = 1;
		}

		return velocity.y;
	}

	public string GetDirectionAiming()
	{
		if (currentDirectionAiming == "Right") 
		{
			if ((weapon.transform.eulerAngles.z < 110 && weapon.transform.eulerAngles.z >= 0) || (weapon.transform.eulerAngles.z <= 360 && weapon.transform.eulerAngles.z > 250)) 
			{
				currentDirectionAiming = "Right";
				return "Right";
			} 
			else 
			{
				currentDirectionAiming = "Left";
				return "Left";
			} 
		} 
		else if (currentDirectionAiming == "Left") 
		{
			if ((weapon.transform.eulerAngles.z > 70 && weapon.transform.eulerAngles.z <= 180) || (weapon.transform.eulerAngles.z >= 180 && weapon.transform.eulerAngles.z < 290)) 
			{
				currentDirectionAiming = "Left";
				return "Left";
			} 
			else 
			{
				currentDirectionAiming = "Right";
				return "Right";
			} 
		} 
		else 
		{
			return "Right";
		}
	}
}
