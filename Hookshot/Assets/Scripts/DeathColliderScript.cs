using UnityEngine;
using System.Collections;

public class DeathColliderScript : MonoBehaviour {
	
	void OnTriggerEnter2D(Collider2D collider)
	{
		PlayerScript player = this.GetComponent<PlayerScript> ();
		if (collider.name == "DeathCollider" && player != null) 
		{
			Destroy (player.gameObject);
		}
	}
}
