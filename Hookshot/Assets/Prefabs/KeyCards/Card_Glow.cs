using UnityEngine;
using System.Collections;

public class Card_Glow : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	float opaque = 1.0f;

	float alpha = 1.0f;

	// Update is called once per frame
	void Update () {
		
		lightPulse ();

	}

	void lightPulse (){

		float brightness = Mathf.PingPong (Time.time, opaque);


		alpha = Mathf.Lerp (0.2f, 0.8f, brightness);
		SpriteRenderer rend = GetComponent <SpriteRenderer> ();
		Color _color = rend.color;
		_color.a = alpha;
		rend.color = _color;
	

	}

}

