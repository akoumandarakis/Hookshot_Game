using UnityEngine;
using System.Collections;
using System;
using Prime31;

[RequireComponent(typeof(Rigidbody2D))]
public class HookShotScript : MonoBehaviour {

    //Player aspects
    private PlayerScript playerScript;
    private Transform parent;
    private CharacterController2D playerController;

    //hookshot speed
    public float speed = 1;
    //retract speed on latch
    public float retractSpeed = 2;
    //maximum distance from player
    public float maxDistanceFromPlayer = 1;
    //distance at which hook will snap back to origin on retract
    public float minRetractDistance = 10;

    //unit vector in which it was aimed
    private Vector3 direction;
    //direction to give to the player script
    private Vector3 handOffDirection;

    //do not set these
    private bool fired;
    private bool latched;
    private bool blocked;

    private Vector3 velocity;
    private Vector3 ZeroVector = new Vector3(0, 0, 0);

    // Use this for initialization
    void Start () {
        playerScript = GetComponentInParent<PlayerScript>();
        playerController = GetComponentInParent<CharacterController2D>();

        parent = transform.parent;

        fired = false;
        latched = false;
        blocked = false;

		playerScript.hookRetractSpeed = retractSpeed;
    }
	
	// Update is called once per frame
	void Update () {

        if (!fired && Input.GetButtonDown("Fire2"))
        {
			this.gameObject.layer = LayerMask.NameToLayer ("Trigger");
            fired = true;
            transform.parent = null;

            Vector3 mousePOS = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			float xDif = playerScript.transform.position.x - mousePOS.x;
			float yDif = playerScript.transform.position.y - mousePOS.y;

			float angleOfDirectionVector = Mathf.Atan2 (yDif, xDif);

			//Set the direction
			direction = new Vector3 (Mathf.Cos (angleOfDirectionVector), Mathf.Sin (angleOfDirectionVector), 0);
			direction *= -1;

			transform.eulerAngles = new Vector3 (0, 0, Mathf.Atan2 (yDif, xDif) * Mathf.Rad2Deg);
        }

        if (fired && Input.GetButtonDown("Jump") && latched)
        {
			playerController.velocity.y = playerScript.Jump (playerController.velocity);
            ResetHookshot();
        }

        if (fired)
        {
            //if the hook has reached max distance it is the same as being blocked
            Vector3 HookToPlayer = transform.position - playerScript.transform.position;
            if (HookToPlayer.magnitude > maxDistanceFromPlayer)
            {
                blocked = true;
            }
        }

        //if the object hit something that isn't hookable
        if (blocked)
        {
            Retract();
        }
        else if (fired && !latched)
        {
            Move();
        }
        else if (fired && latched)
        {
            Retract();
        }

        playerScript.hookLatched = latched;
    }

    private void Retract()
    {
        if (blocked)
        {
            //direction is now directed to the player
            direction = playerScript.transform.position - transform.position;


            if (direction.magnitude < minRetractDistance)
            {
                ResetHookshot();
            }
            else
            {
                direction.Normalize();
                Move();
            }
        }
        else if (latched)
        {
            handOffDirection = transform.position - playerScript.transform.position;

            handOffDirection.z = 0;

            //handOffDirection.Normalize();

            handOffDirection.z = transform.position.z;

            handOffDirection *= retractSpeed;

            playerScript.hookshotAdjust = handOffDirection;
        }
        else
        {
            throw new ArgumentException("Retract called when both latch and blocked are false");
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
		if (collider.gameObject.tag == "Hookable") {
			if (!blocked) {
				latched = true;
			}
		} else if (collider.gameObject.tag != "Player" && collider.gameObject.tag != "HookshotSeeThrough") 
		{
			blocked = true;
		} 
		else if (collider.gameObject.tag == "Player" && latched) 
		{
			blocked = true;
		} 
    }

    private void Move()
    {

        //Transforms the object based on its velocity
        velocity = new Vector3(speed * direction.x, speed * direction.y, 0);

        velocity *= Time.deltaTime;

        transform.Translate(velocity, Space.World);
    }

    private void ResetHookshot()
    {
        transform.position = playerScript.transform.position;

        playerScript.hookshotAdjust = ZeroVector;

        transform.parent = parent;
         
        blocked = false;
        fired = false;
        latched = false;
		this.gameObject.layer = LayerMask.NameToLayer ("Default");
    }

}
