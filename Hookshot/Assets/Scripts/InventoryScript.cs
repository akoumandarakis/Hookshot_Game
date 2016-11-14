using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryScript : MonoBehaviour {

	public List<string> inventory;

	void OnTriggerEnter2D(Collider2D collider)
	{
		PickupInfoScript pickupInfo = collider.gameObject.GetComponent<PickupInfoScript>();
		HealthScript health = this.gameObject.GetComponent<HealthScript> ();
		if (pickupInfo != null)
		{
			if (pickupInfo.KeyCard) 
			{
				inventory.Add (pickupInfo.NameOfKeyCard);
				Destroy (pickupInfo.gameObject);
			}
			else if (pickupInfo.Health && health != null && !health.isEnemy)
			{
				health.hp += pickupInfo.AmountOfHealth;

				if (health.hp > health.maxHP) 
				{
					health.hp = health.maxHP;
				}

				Destroy(pickupInfo.gameObject);
			}


		}
	}
}
