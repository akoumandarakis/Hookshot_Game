using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour
{

    public int hp = 2;
    [HideInInspector]
    public int maxHP;
    public bool isEnemy = true;
	public bool isPlatform = false;

	public ParticleSystem deathParticles;
	public ParticleSystem hitParticles;

	public AudioSource damageSound;
	public AudioSource missleDamageSound;

    void Start()
    {
        maxHP = hp;
				
	}
	void Update (){
		if (hp <= 0)
		{
			if (deathParticles != null) {
				deathParticles.transform.parent = null;
				deathParticles.transform.position = this.gameObject.transform.position;
				deathParticles.Emit (30);
			}
			Destroy (gameObject);
		}
	}
    void OnTriggerEnter2D(Collider2D collider)
    {
        ShotScript shot = collider.gameObject.GetComponent<ShotScript>();
        if (shot != null)
        {
			if (isPlatform) 
			{
				if (hitParticles != null) {

					hitParticles.transform.position = collider.transform.position;
					hitParticles.Emit (5);
				}

				Destroy (shot.gameObject);
			}
            else if (shot.enemyShot != isEnemy)
            {
				if (hitParticles != null) {
					
					hitParticles.transform.position = this.gameObject.transform.position;
					hitParticles.Emit (5);
				}

				if (missleDamageSound != null && collider.name == "Missle(Clone)") {

					missleDamageSound.Play ();

				} else if (damageSound != null) {

					damageSound.Play ();
				}

                hp -= shot.damage;
                Destroy(shot.gameObject);
            }
        }
    }
}