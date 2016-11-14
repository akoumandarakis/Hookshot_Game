using UnityEngine;
using System.Collections;

public class LightColor : MonoBehaviour {
	SpriteRenderer rend;
	public Color _glowColor;
	// Use this for initialization
	void Start () {
		rend = gameObject.GetComponent<SpriteRenderer> ();
		_glowColor = transform.parent.GetComponent<SpriteRenderer> ().color;
		Color _temp = _glowColor;
		_temp.r = _glowColor.r;
		_temp.g = _glowColor.g;
		_temp.b = _glowColor.b;
		_temp.a = rend.color.a;
		rend.material.color = _temp;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
