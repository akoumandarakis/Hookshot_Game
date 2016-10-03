using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    //player's Rigidbody2D component
    Rigidbody2D PlayerRigidBody;

    //true means that the player is on a solid object
    public bool isGrounded = false;

    //this stores a reference to the gravitational force if there is one
    private Vector2 gravity;
    //how much gravity force changes each update if player isn't grounded
    public Vector2 gravityAcceleration = new Vector2(0, 0);
    //zero vector
    private Vector2 zeroVector = new Vector2(0, 0);

	//initialization
	void Start () {
        PlayerRigidBody = GetComponent<Rigidbody2D>();
        gravity = new Vector2(0, 0);
    }
	
	// Update is called once per frame
	void Update () {

        System.Console.WriteLine(isGrounded.ToString());
        updateGravity();

        //other movement goes here


        //movement update
        transform.Translate(gravity, Space.Self);
    }

    /// <summary>
    /// removes gravity if the player is grounded,
    /// adds gravity if the player is newly ungrounded,
    /// updates gravity otherwise
    /// </summary>
    private void updateGravity()
    {
        //if grounded remove the gravity vector
        if (isGrounded && gravity != zeroVector)
        {
            gravity = zeroVector;
        }
        //if ungrounded and there is no gravity yet, make a gravity vector
        else if (!isGrounded && gravity == zeroVector)
        {
            gravity = new Vector2(0, -.5F);
        }
        else if (!isGrounded)
        {
            gravity = gravity + gravityAcceleration;
        }

    }
}
