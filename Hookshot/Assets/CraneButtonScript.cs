using UnityEngine;
using System.Collections;

public class CraneButtonScript:MonoBehaviour{
	
	// Use this for initialization
	void Start () {
		
	}

	void OnTriggerEnter2D(Collider2D Coll){
		if (Coll.gameObject.tag == "Player") {
			transform.parent.GetComponent<hookablesMove> ()._canPush = true;

		}

	}
	void OnTriggerExit2D(Collider2D Coll){
		if (Coll.gameObject.tag == "Player") {
			transform.parent.GetComponent<hookablesMove> ()._canPush = false;

		}

	}

}
