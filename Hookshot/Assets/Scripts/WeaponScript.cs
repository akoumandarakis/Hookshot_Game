using UnityEngine;
using System.Collections;

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
    /// The rate at which the weapon can fire
    /// </summary>
    public float shotRate;

    /// <summary>
    /// Whether or not the weapon is the players' or an enemy's
    /// </summary>
    public bool enemyWeapon;

    /// <summary>
    /// Whether or not the weapon tracks an object
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

    // Use this for initialization
    void Start()
    {
        shotCooldown = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //Update the rotation of the weapon
        updateRotation();

        //Count down until next shot
        if (shotCooldown > 0)
        {
            shotCooldown -= Time.deltaTime;
        }
			
        //If the left mouse button is clicked and it's a player's weapon, fire the weapon 
        if (Input.GetButtonDown("Fire1") && !enemyWeapon)
        {
            Shoot(false);
        }
    }

    /// <summary>
    /// Creates a shot based on the position and rotation of the weapon
    /// </summary>
    /// <param name="isEnemy">Whether or not the shot is an enemy's shot</param>
    public void Shoot(bool isEnemy)
    {
        //If the shot cooldown has reached 0
		if (shotCooldown <= 0f)
        {
            //Reset the shot cooldown
            shotCooldown = shotRate;

            //Create a new shot at the position of the weapon
			var shot = Instantiate(shotPrefab) as Transform;
            shot.position = new Vector3(transform.position.x + transform.right.x, transform.position.y + transform.right.y, transform.position.z);
            shot.eulerAngles = transform.eulerAngles;

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