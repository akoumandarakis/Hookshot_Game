using UnityEngine;
using System.Collections;

/// <summary>
/// Defines the behavior of a shot
/// </summary>
public class ShotScript : MonoBehaviour {

    /// <summary>
    /// The damage the shot can do
    /// </summary>
    public int damage = 1;
    /// <summary>
    /// Whether or not that shot is an enemy's
    /// </summary>
    public bool enemyShot = false;

	// Use this for initialization
	void Start () {
        //Destroy any shot after ten seconds
        Destroy(gameObject, 10);
	}
}
