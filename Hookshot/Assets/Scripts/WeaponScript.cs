﻿using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Creates a weapon that can generate shots
/// </summary>
public class WeaponScript : MonoBehaviour
{

    /// <summary>
    /// The shot that will be fired by the weapon
    /// </summary>
    public Transform shotPrefab;

	/// <summary>
	/// The list of shot types that the weapon can fire and their ammo count
	/// </summary>
	public List<KeyValuePair<Transform, int>> shotTypes = new List<KeyValuePair<Transform, int>>();

	/// <summary>
	/// Whether or not the weapon can shoot different shot types
	/// </summary>
	public bool CanShootDifferentShots;

	public int shotIndex = 0;

    /// <summary>
    /// The rate at which the weapon can fire
    /// </summary>
    public float shotRate;

    /// <summary>
    /// Whether or not the weapon is the players' or an enemy's
    /// </summary>
    public bool enemyWeapon;

    /// <summary>
    /// Whether or not the weapon tracks the player
    /// </summary>
    public bool tracksObject;

    /// <summary>
    /// The object the weapon tracks
    /// </summary>
    public GameObject objectToTrack;

    /// <summary>
    /// The time until the next shot can be fired
    /// </summary>
    private float shotCooldown;

    /// <summary>
    /// The position of the mouse
    /// </summary>
    private Vector3 mousePos;

    /// <summary>
    /// The position of the screen
    /// </summary>
    private Vector3 screenPos;

    /// <summary>
    /// The object position.
    /// </summary>
    private Vector3 objPos;

	private bool buttonDown;

    // Use this for initialization
    void Start()
    {
        shotCooldown = 0f;
		shotTypes.Add(new KeyValuePair<Transform, int>(shotPrefab, -1)); 

		if (tracksObject) 
		{
			objectToTrack = GameObject.FindGameObjectWithTag ("Player");
		}


    }
	
	// Update is called once per frame
	void Update () {
        //Update the rotation of the weapon
        updateRotation();

		if (CanShootDifferentShots && shotTypes.Count > shotIndex) {
			shotPrefab = shotTypes [shotIndex].Key;
		}

        //Count down until next shot
        if (shotCooldown > 0)
        {
            shotCooldown -= Time.deltaTime;
        }
			
        //If the left mouse button is clicked and it's a player's weapon, fire the weapon 
		if (Input.GetButtonDown("Fire1") && !enemyWeapon)
        {
			buttonDown = true;
        }

		if (Input.GetButtonUp ("Fire1")) {
			buttonDown = false;
		}

		if (Input.GetAxis ("Button Press") > 0 && Input.GetButtonDown("Button Press")) {
			shotIndex++;
			if (shotIndex >= shotTypes.Count)
			{
				shotIndex = 0;
			}
		}

		if (Input.GetAxis ("Button Press") < 0 && Input.GetButtonDown("Button Press")) {
			shotIndex--;
			if (shotIndex < 0)
			{
				shotIndex = shotTypes.Count - 1;
			}
		}

		if (buttonDown) {
			Shoot (false);
		}

		if (enemyWeapon)
		{
			Shoot (true);
		}
    }

    /// <summary>
    /// Creates a shot based on the position and rotation of the weapon
    /// </summary>
    /// <param name="isEnemy">Whether or not the shot is an enemy's shot</param>
    public void Shoot(bool isEnemy)
    {
        //If the shot cooldown has reached 0
		if (shotCooldown <= 0f && (shotTypes[shotIndex].Value > 0 || shotTypes[shotIndex].Value < 0))
        {
            //Reset the shot cooldown
            shotCooldown = shotRate;

            //Create a new shot at the position of the weapon
			var shot = Instantiate(shotPrefab) as Transform;
			Debug.Log (this.transform.right);
			shot.position = new Vector3(transform.position.x + this.transform.right.x/4, transform.position.y + this.transform.right.y/4 - 0.04f, transform.position.z);
			Debug.Log (shot.position);
			shot.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + Random.Range(-10,10));

            ShotScript shotScript = shot.gameObject.GetComponent<ShotScript>();

            //Sets whether or not the shot is an enemy's or the player's
            if (shot != null)
            {
                shotScript.enemyShot = isEnemy;
            }

            //Makes sure the shot moves right relative to the weapon
            MoveScript movement = shot.gameObject.GetComponent<MoveScript>();
            if (movement != null)
            {
                movement.direction = shot.transform.right;
            }

			MoveTowardScript moveToward = shot.gameObject.GetComponent<MoveTowardScript> ();
			if (moveToward != null) 
			{
				moveToward.objectToMoveTowards = GameObject.FindGameObjectWithTag ("Player");
			}

			int numberOfShots = shotTypes [shotIndex].Value - 1;
			shotTypes [shotIndex] = new KeyValuePair<Transform, int>(shotTypes[shotIndex].Key, numberOfShots);
        }
    }

    /// <summary>
    /// Updates the rotation of the player's weapon based on the position of the mouse
    /// </summary>
    public void updateRotation()
    {
        if (!enemyWeapon)
        {
            //Gets the position of the mouse and creates a vector from the that position on the screen
            mousePos = Input.mousePosition;
            screenPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, transform.position.z - Camera.main.transform.position.z));

            //Sets the rotation of the weapon based on that position
            transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((screenPos.y - transform.position.y), (screenPos.x - transform.position.x)) * Mathf.Rad2Deg);
        }
	
        if (tracksObject && objectToTrack != null)
        {
            //Gets the position of the object and creates a vector from the that position on the screen
            objPos = objectToTrack.transform.position;

            //Sets the rotation of the weapon based on that position
            transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((objPos.y - transform.position.y), (objPos.x - transform.position.x)) * Mathf.Rad2Deg);
        }
    }

    public bool CanAttack
    {
        get
        {
            return shotCooldown <= 0f;
        }
    }
}
		