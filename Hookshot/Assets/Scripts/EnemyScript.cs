using UnityEngine;

/// <summary>
/// Enemy generic behavior
/// </summary>
public class EnemyScript : MonoBehaviour
{
    private bool hasSpawn;
    private MoveScript moveScript;
    private WeaponScript[] weapons;
    private Collider2D coliderComponent;
    private SpriteRenderer rendererComponent;
	private SoldierMoveScript soldiermoveScript;

    void Awake()
    {
        // Retrieve the weapon only once
        weapons = GetComponentsInChildren<WeaponScript>();

        // Retrieve scripts to disable when not spawn
        moveScript = GetComponent<MoveScript>();

        coliderComponent = GetComponent<Collider2D>();

        rendererComponent = GetComponent<SpriteRenderer>();

		soldiermoveScript = GetComponent<SoldierMoveScript> ();
    }

    // 1 - Disable everything
    void Start()
    {
        hasSpawn = false;

        // Disable everything
        // -- collider
		if (coliderComponent != null) 
		{
			coliderComponent.enabled = false;
		}
        
        // -- Moving
		if (moveScript != null) 
		{
			moveScript.enabled = false;
		}

		// -- Moving
		if (soldiermoveScript != null) 
		{
			soldiermoveScript.enabled = false;
		}
        
        // -- Shooting
        foreach (WeaponScript weapon in weapons)
        {
			if (weapon != null) 
			{
				weapon.enabled = false;
			}
            
        }
    }

    void Update()
    {
        // 2 - Check if the enemy has spawned.
        if (hasSpawn == false)
        {
            if (rendererComponent.isVisible)
            {
                Spawn();
            }
        }
        else
        {
            // Auto-fire
            foreach (WeaponScript weapon in weapons)
            {
                if (weapon != null && weapon.enabled && weapon.CanAttack)
                {
                    weapon.Shoot(true);
                }
            }

			if (rendererComponent.isVisible == false)
			{
				Disable();
			}
        }
    }

    // 3 - Activate itself.
    private void Spawn()
    {
        hasSpawn = true;

		// Disable everything
		// -- collider
		if (coliderComponent != null) 
		{
			coliderComponent.enabled = true;
		}

		// -- Moving
		if (moveScript != null) 
		{
			moveScript.enabled = true;
		}

		// -- Moving
		if (soldiermoveScript != null) 
		{
			soldiermoveScript.enabled = true;
		}

		// -- Shooting
		foreach (WeaponScript weapon in weapons)
		{
			if (weapon != null) 
			{
				weapon.enabled = true;
			}

		}
    }

	private void Disable()
	{
		hasSpawn = false;

		// Disable everything
		// -- collider
		if (coliderComponent != null) 
		{
			coliderComponent.enabled = false;
		}

		// -- Shooting
		foreach (WeaponScript weapon in weapons)
		{
			if (weapon != null) 
			{
				weapon.enabled = false;
			}

		}
	}
}