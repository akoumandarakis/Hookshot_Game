using UnityEngine;
using System.Collections.Generic;

public class BossScript : MonoBehaviour {

	public List<WeaponScript> AllWeapons;

	public List<WeaponScript> StraightShooters;

	public List<WeaponScript> Shotgun;

	public WeaponScript MachineGun;

	public Transform misslePrefab;

	public float attackRate;

	//public float attackLength;

	private float attackCooldown;
	//public float attackTime;

	public int numberOfMissles;

	private int attackType = 0;

	private int prevAttack = 4;

	private int prevPrevAttack = 0;

	// Use this for initialization
	void Start () {
		foreach (WeaponScript weapon in this.gameObject.GetComponentsInChildren<WeaponScript>()) 
		{
			AllWeapons.Add (weapon);

			if (weapon.gameObject.name.Equals("StraightShooter"))
			{
				StraightShooters.Add(weapon);
			}

			if (weapon.gameObject.name.Equals ("Shotgun")) 
			{
				Shotgun.Add (weapon);
			}

			if (weapon.gameObject.name.Equals ("MachineGun")) 
			{
				MachineGun = weapon;
				MachineGun.objectToTrack = GameObject.FindGameObjectWithTag ("Player");
			}
		}

		DisableAllWeapons ();
		attackCooldown = attackRate;
	}
	
	// Update is called once per frame
	void Update () {

		//Count down until next shot
		if (attackCooldown > 0)
		{
			attackCooldown -= Time.deltaTime;
		}

		if (attackCooldown <= 0) 
		{
			ChooseNextAttack ();

			if (attackType == 1) {
				MachineGunAttack ();
			} else if (attackType == 2) {
				ShotgunAttack ();
			} else if (attackType == 3) {
				StraightShooterAttack ();
			} else if (attackType == 4) {
				MissleAttack ();
			} else if (attackType == 0) {
				DisableAllWeapons ();
			}
			attackCooldown = attackRate;
		}
	}

	public void MachineGunAttack()
	{
		MachineGun.enabled = true;
	}

	public void ShotgunAttack()
	{
		foreach (WeaponScript weapon in Shotgun) 
		{
			weapon.enabled = true;
		}
	}

	public void StraightShooterAttack()
	{
		foreach (WeaponScript weapon in StraightShooters) 
		{
			weapon.shotRate = Random.Range (0.5f, 1f);
			weapon.enabled = true;
		}
	}

	public void MissleAttack()
	{
		for (int i = 0; i <= numberOfMissles; i++) 
		{
			var missle = Instantiate(misslePrefab) as Transform;
			missle.position = new Vector3(transform.position.x, transform.position.y + Random.Range(0f, 1f), transform.position.z);
			MoveTowardScript missleMovement = missle.GetComponent<MoveTowardScript> ();
			if (missleMovement != null) 
			{
				missleMovement.objectToMoveTowards = GameObject.FindGameObjectWithTag ("Player");
			}
		}
	}

	public void DisableAllWeapons()
	{
		foreach (WeaponScript weapon in AllWeapons) 
		{
			weapon.enabled = false;
		}
	}

	public void ChooseNextAttack()
	{
		if (prevAttack != 0 && prevPrevAttack == 0) {
			prevPrevAttack = prevAttack;
			prevAttack = attackType;

			if (prevPrevAttack == 1) {
				attackType = 2;
			} else if (prevPrevAttack == 2) {
				attackType = 3;
			} else if (prevPrevAttack == 3) {
				attackType = 4;
			} else {
				attackType = 1;
			}


		} 
		else if (prevAttack == 0 && prevPrevAttack != 0) 
		{
			prevPrevAttack = prevAttack;
			prevAttack = attackType;
			attackType = 0;

		}
	}
}
