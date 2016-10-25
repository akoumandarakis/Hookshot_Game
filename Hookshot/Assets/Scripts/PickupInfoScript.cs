using UnityEngine;
using System.Collections;

public class PickupInfoScript : MonoBehaviour {

	/// <summary>
	/// The name of the Keycard.
	/// </summary>
	public string NameOfKeyCard;

	/// <summary>
	/// Whether or not the pickup is a keycard.
	/// </summary>
	public bool KeyCard;

	/// <summary>
	/// Whether or not the pickup is a health pack.
	/// </summary>
	public bool Health;

	/// <summary>
	/// The amount of health that the health pickup restores. 
	/// </summary>
	public int AmountOfHealth;
}
