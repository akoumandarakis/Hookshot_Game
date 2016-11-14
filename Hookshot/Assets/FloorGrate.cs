using UnityEngine;
using System.Collections;

public class FloorGrate : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollision2DEnter(Collision2D coll) {
		Rigidbody2D rigid = gameObject.GetComponent<Rigidbody2D> ();
		rigid.gravityScale = 1.0f;
	}
}
