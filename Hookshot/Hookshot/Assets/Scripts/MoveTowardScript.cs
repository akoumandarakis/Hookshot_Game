using UnityEngine;
using System.Collections;

public class MoveTowardScript : MonoBehaviour {

	/// <summary>
	/// The speed that the object will move with
	/// </summary>
	public Vector2 acceleration;

	/// <summary>
	/// The direction that the object will move in
	/// </summary>
	private Vector2 direction;

	/// <summary>
	/// The speed of the object
	/// </summary>
	public Vector2 speed = new Vector2(0f,0f);

	/// <summary>
	/// The speed multiplied by the direction
	/// </summary>
	private Vector2 velocity;

	public GameObject objectToMoveTowards;

	// Update is called once per frame
	void Update () {
		speed = new Vector2(speed.x + acceleration.x, speed.y + acceleration.y);

		//Gets the position of the object
		Vector3 objPos = objectToMoveTowards.transform.position;

		//Sets the rotation of the weapon based on that position
		transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((objPos.y - transform.position.y), (objPos.x - transform.position.x)) * Mathf.Rad2Deg);

		direction = this.transform.right;

		velocity = new Vector3 (speed.x * direction.x, speed.y * direction.y, 0);

		velocity *= Time.deltaTime;
		transform.Translate(velocity, Space.World);
	}
}
