using UnityEngine;
using System.Collections.Generic;

public class BossScript : MonoBehaviour {

	public List<WeaponScript> AllWeapons;

	public List<WeaponScript> StraightShooters;

	public List<WeaponScript> Shotgun;

	public WeaponScript MachineGun;

	public Transform misslePrefab;

	public float attackLength;

	//public float attackLength;
	public float chargeLength;

	private float attackCooldown;
	//public float attackTime;

	public AudioSource missleFireSound;
	public AudioSource missleDamageSound;
	public AudioSource ChargeUp;


	public int numberOfMissles;

	private int attackType = 0;

	private int prevAttack = 4;

	private int prevPrevAttack = 0;

	private bool FirstCharge = true;

	private bool IsDying = false;
	private float DeathTimer = 6f;

	public ParticleSystem death1;
	public ParticleSystem death2;
	public ParticleSystem death3;
	public ParticleSystem death4;
	public ParticleSystem death5;
	public ParticleSystem death6;
	public ParticleSystem death7;
	public ParticleSystem death8;
	public ParticleSystem death9;
	public ParticleSystem death10;

	public AudioSource deathSound;
	public AudioSource bigBoom;
	private bool notPlayed = true;


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
		attackCooldown = chargeLength;
	}
	
	// Update is called once per frame
	void Update () {

		if (IsDying) {

			DisableAllWeapons ();
			if (notPlayed) {
				deathSound.Play ();
				notPlayed = false;
			}

			if (DeathTimer > 0) {
				DeathTimer -= Time.deltaTime;
			}

			if (DeathTimer <= 5 && DeathTimer >= 4) {
				death1.Emit (30);
				
			}
			if (DeathTimer <= 4 && DeathTimer >= 3) {
				death2.Emit (30);
			}
			if (DeathTimer <= 3 && DeathTimer >= 2) {
				death3.Emit (30);
				death4.Emit (30);
			}
			if (DeathTimer <= 2 && DeathTimer >= 1) {
				death5.Emit (30);
				death6.Emit (30);
				death7.Emit (30);
				death8.Emit (30);
			}
			if (DeathTimer <= 0) {
				death9.Emit (200);
				death10.Emit (200);
				bigBoom.Play ();
				Destroy (this.gameObject);
			}
		}
			
			if (FirstCharge) {
				ChargeUp.Play ();
				FirstCharge = false;
			}

			//Count down until next shot
		if (attackCooldown > 0) {
			attackCooldown -= Time.deltaTime;
		}

		if (attackCooldown <= 0) {
			ChooseNextAttack ();

			if (attackType == 1) {
				MachineGunAttack ();
				attackCooldown = attackLength;
			} else if (attackType == 2) {
				ShotgunAttack ();
				attackCooldown = attackLength;
			} else if (attackType == 3) {
				StraightShooterAttack ();
				attackCooldown = attackLength;
			} else if (attackType == 4) {
				MissleAttack ();
				attackCooldown = attackLength;
			} else if (attackType == 0) {
				DisableAllWeapons ();
				if (!IsDying) {
					ChargeUp.Play ();
				}
				attackCooldown = chargeLength;
			}
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
		if (missleFireSound != null) {
			missleFireSound.Play ();
		}
		for (int i = 0; i <= numberOfMissles; i++) 
		{
			var missle = Instantiate(misslePrefab) as Transform;
			missle.position = new Vector3(transform.position.x - Random.Range(-0.5f, -1.5f), transform.position.y + 0.5f, transform.position.z);
			MoveTowardScript missleMovement = missle.GetComponent<MoveTowardScript> ();
			HealthScript missleHealth = missle.GetComponent<HealthScript> ();
			missle.GetComponent<EnemyScript> ().enabled = false;
			if (missleMovement != null) 
			{
				missleMovement.objectToMoveTowards = GameObject.FindGameObjectWithTag ("Player");
			}
			if (missleHealth != null && missleDamageSound != null) 
			{
				missleHealth.damageSound = missleDamageSound;
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

	public void StartDeath()
	{
		IsDying = true;
	}
}
