using UnityEngine;
using System.Collections;
using System;
using Prime31;

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
    //distance at which hook will consider the player successfully pulled to target
    public float minLatchRetractDistance = 2;

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
    }
	
	// Update is called once per frame
	void Update () {

        if (!fired && Input.GetButtonDown("Fire2"))
        {
            fired = true;
            transform.parent = null;

            Vector3 mousePOS = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            mousePOS.z = 0;

            //make a normalized vector for direction
            direction = mousePOS - playerScript.transform.position;
            direction.Normalize();
            direction.z = transform.position.z;
        }

        if (fired && Input.GetButtonDown("Jump") && latched)
        {
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

            if (handOffDirection.magnitude < minLatchRetractDistance)
            {
                ResetHookshot();
            }
            else
            {
                handOffDirection.Normalize();

                handOffDirection.z = transform.position.z;

                handOffDirection *= retractSpeed;

                playerScript.hookshotAdjust = handOffDirection;
                Debug.Log(handOffDirection.ToString());
            }
        }
        else
        {
            throw new ArgumentException("Retract called when both latch and blocked are false");
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Hookable")
        {
            latched = true;
        }
    }

    private void Move()
    {

        //Transforms the object based on its velocity
        velocity = new Vector3(speed * direction.x, speed * direction.y, 0);

        velocity *= Time.deltaTime;

        transform.Translate(velocity, Space.Self);
    }

    private void ResetHookshot()
    {
        transform.position = playerScript.transform.position;

        playerScript.hookshotAdjust = ZeroVector;

        transform.parent = parent;
         
        blocked = false;
        fired = false;
        latched = false;
    }

}
