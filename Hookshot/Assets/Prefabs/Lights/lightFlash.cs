using UnityEngine;
using System.Collections;

public class lightFlash : MonoBehaviour {
	public float speed;
	public float brightness;

	Renderer rend; 

	Color _tempColor;

	int direction = 1;

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer> ();

	}

	// Update is called once per frame
	void Update () {
	
		if (rend.material.color.a >= brightness || rend.material.color.a <= 0.1f) {

			direction = direction * -1;

		}
		_tempColor = rend.material.color;
		_tempColor.a = _tempColor.a + (direction * (_tempColor.a * (speed * Time.deltaTime)));
		rend.material.color = _tempColor;
	
	}
}
