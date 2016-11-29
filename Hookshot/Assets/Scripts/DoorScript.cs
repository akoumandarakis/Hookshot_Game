using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {

	public string KeyToOpen;

	public GameObject door;

	void OnTriggerEnter2D (Collider2D collider) 
	{
		InventoryScript playerInventory = collider.gameObject.GetComponent<InventoryScript> ();
		AnimationController2D animator = door.GetComponent<AnimationController2D> ();
		if (playerInventory != null && door != null) 
		{
			foreach (string keycard in playerInventory.inventory) 
			{
				if (keycard.Equals (KeyToOpen)) 
				{
					if (animator != null) 
					{
						animator.setAnimation ("door open");
					}
					door.layer = LayerMask.NameToLayer("Default");
				}
			}
		}
	}
}
