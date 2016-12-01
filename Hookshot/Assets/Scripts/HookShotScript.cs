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

	private SpriteRenderer visibility;

	public bool OnPlayer = false;

	private LineRenderer lineRenderer;

	private WeaponScript weapon;

	public ParticleSystem hookshotFlash;

	public AudioClip firedSound;
	public AudioClip latchedSound;

    // Use this for initialization
    void Start () {
        playerScript = GetComponentInParent<PlayerScript>();
        playerController = GetComponentInParent<CharacterController2D>();
		weapon = playerScript.gameObject.GetComponentInChildren<WeaponScript> ();

		lineRenderer = this.gameObject.GetComponent<LineRenderer> ();
		lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
		lineRenderer.SetColors (new Color (200, 100, 200, 255), new Color (200, 0, 200, 0));
		lineRenderer.sortingLayerName = "player";
		lineRenderer.sortingOrder = -1;
		lineRenderer.SetWidth(0.01F, 0.01F);
		lineRenderer.SetVertexCount(2);

        parent = transform.parent;

        fired = false;
        latched = false;
        blocked = false;

		playerScript.hookRetractSpeed = retractSpeed;

		visibility = GetComponentInParent<SpriteRenderer> ();
		if (visibility != null) {
			visibility.enabled = false;
		}
    }
	
	// Update is called once per frame
	void Update () {

        if (!fired && Input.GetButtonDown("Fire2"))
        {
			AudioSource.PlayClipAtPoint (firedSound, this.transform.position);

			if (hookshotFlash != null) {
				hookshotFlash.transform.position = new Vector3(weapon.gameObject.transform.position.x + weapon.gameObject.transform.right.x/4, weapon.gameObject.transform.position.y + weapon.gameObject.transform.right.y/4 - 0.04f, weapon.gameObject.transform.position.z);
				hookshotFlash.transform.eulerAngles = weapon.gameObject.transform.eulerAngles;
				hookshotFlash.Emit(5);
			}

			this.gameObject.layer = LayerMask.NameToLayer ("Trigger");
			if (visibility != null) {
				visibility.enabled = true;
			}
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
		if (Input.GetButtonDown("Fire2") && latched)
        {
			ResetHookshot ();
			AudioSource.PlayClipAtPoint (firedSound, this.transform.position);

			if (hookshotFlash != null) {
				hookshotFlash.transform.position = new Vector3(weapon.gameObject.transform.position.x + weapon.gameObject.transform.right.x/4, weapon.gameObject.transform.position.y + weapon.gameObject.transform.right.y/4 - 0.04f, weapon.gameObject.transform.position.z);
				hookshotFlash.transform.eulerAngles = weapon.gameObject.transform.eulerAngles;
				hookshotFlash.Emit(5);
			}

			this.gameObject.layer = LayerMask.NameToLayer ("Trigger");
			if (visibility != null) {
				visibility.enabled = true;
			}
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

        if (fired && Input.GetButtonDown ("Jump") && latched && !playerScript.IsZeroG) {
		playerController.velocity.y = playerScript.Jump (playerController.velocity);
		ResetHookshot ();
		} 
		else if (fired && Input.GetButtonDown ("Jump") && latched) 
		{
			ResetHookshot ();
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
			lineRenderer.SetPosition(0, new Vector3(this.gameObject.transform.position.x ,this.gameObject.transform.position.y,this.gameObject.transform.position.z));
			lineRenderer.SetPosition(1, new Vector3(playerScript.gameObject.transform.position.x, playerScript.gameObject.transform.position.y, playerScript.gameObject.transform.position.z));

			direction = playerScript.transform.position - transform.position;


			if (!OnPlayer) {
				handOffDirection = transform.position - playerScript.transform.position;

				handOffDirection.z = 0;

				//handOffDirection.Normalize();

				handOffDirection.z = transform.position.z;

				handOffDirection *= retractSpeed;

				playerScript.hookshotAdjust = handOffDirection;
			} 
			else 
			{
				playerScript.hookshotAdjust = ZeroVector;
			}
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
				this.gameObject.transform.parent = collider.transform;
				latched = true;
				AudioSource.PlayClipAtPoint (latchedSound, this.transform.position);
			}
		} else if (collider.gameObject.tag != "Player" && collider.gameObject.tag != "HookshotSeeThrough") 
		{
			blocked = true;
		} 
		else if (collider.gameObject.tag == "Player" && latched) 
		{
			//blocked = true;
			OnPlayer = true;
		} 
    }

	void OnTriggerExit2D(Collider2D collider)
	{
		if (collider.gameObject.tag == "Player" && latched) 
		{
			OnPlayer = false;
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
		lineRenderer.SetPosition(0, new Vector3(this.gameObject.transform.position.x,this.gameObject.transform.position.y,this.gameObject.transform.position.z));
		lineRenderer.SetPosition(1, new Vector3(this.gameObject.transform.position.x,this.gameObject.transform.position.y,this.gameObject.transform.position.z));

        transform.position = playerScript.transform.position;

        playerScript.hookshotAdjust = ZeroVector;

        transform.parent = parent;
         
        blocked = false;
        fired = false;
        latched = false;
		OnPlayer = false;
		this.gameObject.layer = LayerMask.NameToLayer ("Default");
		if (visibility != null) {
			visibility.enabled = false;
		}
    }

}
