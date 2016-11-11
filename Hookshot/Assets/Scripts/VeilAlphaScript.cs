using UnityEngine;
using System.Collections;

public class VeilAlphaScript : MonoBehaviour {

	public float lerpSpeed = 2.0f;
	float direction;
	SpriteRenderer rend;
	// Use this for initialization
	void Start () {
		rend = gameObject.GetComponent <SpriteRenderer> ();
	}

	// Update is called once per frame
	void Update () {
		if (direction == 1.0f) {
			alphaChange ();

		
		}
		if (direction == -1.0f) {
			alphaChange ();
		}
	}

	void OnTriggerEnter2D (Collider2D Coll) {

		if(Coll.gameObject.tag == "Player") {
			direction = 1.0f;

			//1.0f - (lerpSpeed * Time.deltaTime)
		}
	
	
	}
	void OnTriggerExit2D (Collider2D Coll) {

		if (Coll.gameObject.tag == "Player") {
			direction = -1.0f;
		}
	}
	void alphaChange (){
		Color _tempColor = rend.color;
		_tempColor.a = Mathf.MoveTowards(_tempColor.a, ((-direction + 1.0f) * 0.5f), (lerpSpeed * Time.deltaTime));
		rend.color = _tempColor;

	}
}
