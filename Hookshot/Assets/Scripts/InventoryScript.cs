using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryScript : MonoBehaviour {

	public List<string> inventory;
	public AudioSource pickupSound;

	void OnTriggerEnter2D(Collider2D collider)
	{
		PickupInfoScript pickupInfo = collider.gameObject.GetComponent<PickupInfoScript>();
		HealthScript health = this.gameObject.GetComponent<HealthScript> ();
		WeaponScript weapon = this.gameObject.GetComponentInChildren<WeaponScript> ();
		if (pickupInfo != null)
		{
			if (pickupInfo.KeyCard) 
			{
				inventory.Add (pickupInfo.NameOfKeyCard);
				pickupSound.Play ();
				Destroy (pickupInfo.gameObject);
			}
			else if (pickupInfo.Health && health != null && !health.isEnemy && health.hp < health.maxHP)
			{
				health.hp += pickupInfo.AmountOfHealth;

				if (health.hp > health.maxHP) 
				{
					health.hp = health.maxHP;
				}
				pickupSound.Play ();
				Destroy(pickupInfo.gameObject);
			}
			else if (pickupInfo.Ammo)
			{
				int index = 0;
				pickupSound.Play ();
				foreach (KeyValuePair<Transform, int> shotType in weapon.shotTypes) 
				{
					if (pickupInfo.AmmoType.gameObject.name == shotType.Key.gameObject.name) 
					{
						int numShots = shotType.Value;
						weapon.shotTypes.Remove (shotType);
						weapon.shotTypes.Insert (index, new KeyValuePair<Transform, int> (pickupInfo.AmmoType, pickupInfo.NumberOfShotsGiven + numShots));
						Destroy (pickupInfo.gameObject);
						return;
					}
					index++;
				}
				weapon.shotTypes.Add (new KeyValuePair<Transform, int> (pickupInfo.AmmoType, pickupInfo.NumberOfShotsGiven));
				Destroy(pickupInfo.gameObject);
			}


		}
	}
}
