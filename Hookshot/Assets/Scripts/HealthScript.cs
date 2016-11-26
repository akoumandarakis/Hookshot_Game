using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour
{

    public int hp = 2;
    [HideInInspector]
    public int maxHP;
    public bool isEnemy = true;
	public bool isPlatform = false;

    void Start()
    {
        maxHP = hp;
				
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

        if (hp <= 0)
        {
			Destroy(gameObject);
        }
    }
}