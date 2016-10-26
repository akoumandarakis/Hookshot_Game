using UnityEngine;
using System.Collections;

public class VeilAlphaScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}



	float lerpSpeed = 1.0f;

	// Update is called once per frame
	void Update () {
		

		
		}



	void OnCollisionEnter2D (Collision2D Coll) {

		if(Coll.gameObject.tag == "Player"); {


			SpriteRenderer rend = GetComponent <SpriteRenderer> ();
			Color _tempcolor = rend.color;
			_tempcolor.a = Mathf.Lerp (1.0f, 0.0f,Time.deltaTime);
			rend.color = _tempcolor;


		}

	}
}
