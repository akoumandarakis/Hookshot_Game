using UnityEngine;
using System.Collections;

public class SoldierMoveScript : MonoBehaviour {

    /// <summary>
    /// The speed that the object will move with
    /// </summary>
    public Vector2 speed = new Vector2(5, 5);

    /// <summary>
    /// The direction that the object will move in
    /// </summary>
    public Vector2 direction = new Vector2(1, 0);

    /// <summary>
    /// The speed multiplied by the direction
    /// </summary>
    private Vector3 velocity;

    /// <summary>
    /// Time before the soldier starts moving the opposite direction
    /// </summary>
    public float patrolRate;

	private float patrolCooldown;

    void Start()
    {
        patrolCooldown = patrolRate;
    }

    // Update is called once per frame
    void Update()
    {

        if (patrolCooldown > 0)
        {
            patrolCooldown -= Time.deltaTime;
        }
        else
        {
            direction = new Vector2(direction.x * -1, direction.y);
            patrolCooldown = patrolRate;
        }
        
        //Transforms the object based on its velocity
        velocity = new Vector3(speed.x * direction.x, speed.y * direction.y, 0);

        velocity *= Time.deltaTime;
        transform.Translate(velocity);
    }
}
