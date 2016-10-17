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

    /// <summary>
    /// The time a shot can travel before being destroyed
    /// </summary>
    public float range;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, range);
	}
}
