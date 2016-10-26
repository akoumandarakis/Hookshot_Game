using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {

	public string KeyToOpen;

	public GameObject door;
	
	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D collider) 
	{
		InventoryScript playerInventory = collider.gameObject.GetComponent<InventoryScript> ();
		if (playerInventory != null && door != null) 
		{
			foreach (string keycard in playerInventory.inventory) 
			{
				if (keycard.Equals (KeyToOpen)) 
				{
					door.layer = LayerMask.NameToLayer("Default");
				}
			}
		}
	}
}
