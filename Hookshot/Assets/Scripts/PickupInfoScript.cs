using UnityEngine;
using System.Collections;

public class PickupInfoScript : MonoBehaviour {
	/// <summary>
	/// Whether or not the pickup is a keycard.
	/// </summary>
	public bool KeyCard;

	/// <summary>
	/// Whether or not the pickup is a health pack.
	/// </summary>
	public bool Health;

	/// <summary>
	/// Whether or not the pickup is ammo
	/// </summary>
	public bool Ammo;

	/// <summary>
	/// The name of the Keycard.
	/// </summary>
	public string NameOfKeyCard;

	/// <summary>
	/// The amount of health that the health pickup restores. 
	/// </summary>
	public int AmountOfHealth;

	/// <summary>
	/// The type of the ammo
	/// </summary>
	public Transform AmmoType;

	/// <summary>
	/// The number of shots each ammo pickup gives to the player
	/// </summary>
	public int NumberOfShotsGiven;
}
