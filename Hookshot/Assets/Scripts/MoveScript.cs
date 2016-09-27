using UnityEngine;
using System.Collections;

/// <summary>
/// Moves an object based on the given speed and direction
/// </summary>
public class MoveScript : MonoBehaviour {

    /// <summary>
    /// The speed that the object will move with
    /// </summary>
    public Vector2 speed = new Vector2(5, 5);

    /// <summary>
    /// The direction that the object will move in
    /// </summary>
    public Vector2 direction = new Vector2(1, 0);

	/// <summary>
	/// Whether the object moves relative to itself or to the world
	/// </summary>
	public bool moveRelativeToSelf;
    
    /// <summary>
    /// The speed multiplied by the direction
    /// </summary>
    private Vector3 velocity;

	// Update is called once per frame
	void Update () {
        //Transforms the object based on its velocity
        velocity = new Vector3(speed.x * direction.x, speed.y * direction.y, 0);

        velocity *= Time.deltaTime;
		if (moveRelativeToSelf) 
		{
			transform.Translate(velocity, Space.Self);
		} 
		else 
		{
			transform.Translate(velocity, Space.World);
		}

        
	}
}
