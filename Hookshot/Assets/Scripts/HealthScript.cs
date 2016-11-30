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

    void Start()
    {
        maxHP = hp;
				
	}
	void Update (){
		if (hp <= 0)
		{
			if (deathParticles != null) {
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
				Destroy (shot.gameObject);
			}
            else if (shot.enemyShot != isEnemy)
            {
                hp -= shot.damage;
                Destroy(shot.gameObject);
            }
        }
    }
}