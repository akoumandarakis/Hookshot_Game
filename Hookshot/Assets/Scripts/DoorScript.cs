using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {

	public string KeyToOpen;

	public GameObject door;

	public GameObject doorLight;

	Color Red = new Color(.807f, .058f, .058f, 1.0f);

	Color Green = new Color(.09f, .756f, .396f, 1.0f);

	SpriteRenderer rend;

	void Start(){
		rend = doorLight.GetComponent<SpriteRenderer> ();
		rend.color = Red;
	}
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
					rend.color = Green;
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
