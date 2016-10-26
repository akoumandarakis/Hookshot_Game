using UnityEngine;
using System.Collections;

public class VeilAlphaScript : MonoBehaviour {

	SpriteRenderer rend;
	float lerpSpeed = 1.0f;
	float _alpha = 0.0f;
	public float startTime = 0.0f;

	// Use this for initialization
	void Start () {

		rend = GetComponent <SpriteRenderer> ();
	
	}

	// Update is called once per frame
	void Update () {
		

	}
	void OnTriggerStay2d (Collision2D col){
	
		Debug.Log ("enter");

	}
	void OnCollisionEnter2D (Collision2D Coll) {

		startTime = Time.time;
		Debug.Log ("enter");


	}
	void OnCollisionStay2D (Collision2D Coll) {
		Debug.Log ("stay");
		if(Coll.gameObject.tag == "Player") {

			if (rend.color.a >= 0.0f) {

				Color _tempcolor = rend.color;
				_tempcolor.a = _tempcolor.a - Time.deltaTime;

				//Color _tempcolor = rend.color;
				//Debug.Log (_tempcolor);
				//StartCoroutine ("Fade");


			}

		}
			
	}

	public IEnumerator Fade (){
		Debug.Log ("in coroutine");
		Color _tempcolor = rend.color;

		_tempcolor.a = Mathf.SmoothStep (_tempcolor.a, 0.0f, Time.time - startTime);
		rend.color = _tempcolor;
		yield return _tempcolor.a;
	}
}
