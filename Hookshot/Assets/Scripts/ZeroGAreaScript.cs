using UnityEngine;
using System.Collections;

public class ZeroGAreaScript : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D collider)
	{
		PlayerScript player = this.GetComponent<PlayerScript> ();
		if (collider.name == "ZeroGArea" && player != null) 
		{
			player.IsZeroG = true;

		}
	}

	void OnTriggerExit2D(Collider2D collider)
	{
		PlayerScript player = this.GetComponent<PlayerScript> ();
		if (collider.name == "ZeroGArea" && player != null) 
		{
			player.IsZeroG = false;
		}
	}
}
