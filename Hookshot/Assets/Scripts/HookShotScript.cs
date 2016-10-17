using UnityEngine;
using System.Collections;
using System;

public class HookShotScript : MonoBehaviour {

    //Player Rigid body
    private PlayerScript player;

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
        player = GetComponentInParent<PlayerScript>();
        fired = false;
        latched = false;
        blocked = false;
    }
	
	// Update is called once per frame
	void Update () {

        if (!fired && Input.GetButtonDown("Fire2"))
        {
            fired = true;

            Vector3 mousePOS = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //make a normalized vector for direction
            direction = mousePOS - player.transform.position;
            direction.Normalize();
        }

        if (fired && Input.GetButtonDown("Jump"))
        {
            ResetHookshot();
        }

        if (fired)
        {
            //if the hook has reached max distance it is the same as being blocked
            Vector3 HookToPlayer = transform.position - player.transform.position;
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

    }

    private void Retract()
    {
        if (blocked)
        {
            //direction is now directed to the player
            direction = player.transform.position - transform.position;

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
            handOffDirection = transform.position - player.transform.position;
            handOffDirection /= retractSpeed;

            player.hookshotAdjust = handOffDirection;
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
        transform.position = player.transform.position;

        player.hookshotAdjust = ZeroVector;
         
        blocked = false;
        fired = false;
        latched = false;
    }

}
