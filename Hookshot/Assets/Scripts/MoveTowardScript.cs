﻿using UnityEngine;
using System.Collections;

public class MoveTowardScript : MonoBehaviour {

	/// <summary>
	/// The speed that the object will move with
	/// </summary>
	public Vector2 acceleration;

	public float accelerationConstant;

	/// <summary>
	/// The direction that the object will move in
	/// </summary>
	private Vector2 direction;

	/// <summary>
	/// The speed multiplied by the direction
	/// </summary>
	public Vector2 velocity = new Vector2(0f, 0f);

	public GameObject objectToMoveTowards;

	// Update is called once per frame
	void Update () {
		//Gets the position of the object to accelerate towards
		Vector3 objPos = objectToMoveTowards.transform.position;

		//The distance in the x and y axises from the object to chase to this object
		float yDif = objPos.y - transform.position.y;
		float xDif = objPos.x - transform.position.x;

		//The angle of the vector (in RADIANS) pointing from this object to the object to chase
		float angleOfDirectionVector = Mathf.Atan2 (yDif, xDif);

		//Set the direction
		direction = new Vector2 (Mathf.Cos (angleOfDirectionVector), Mathf.Sin(angleOfDirectionVector));

		//Set the acceleration vector 
		acceleration = new Vector2(accelerationConstant * direction.x, accelerationConstant * direction.y);

		acceleration *= Time.deltaTime;

		velocity = new Vector2(velocity.x + acceleration.x, velocity.y + acceleration.y);

		transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg);

		//velocity *= Time.deltaTime;
		transform.Translate(velocity, Space.World);
	}
}
