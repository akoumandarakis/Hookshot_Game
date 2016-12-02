using UnityEngine;
using System.Collections;

public class CraneButtonScript:MonoBehaviour{

	private GameObject Q;
	
	// Use this for initialization
	void Start () {
		Q = transform.FindChild ("Q").gameObject;
		
	}

	void OnTriggerEnter2D(Collider2D Coll){
		if (Coll.gameObject.tag == "Player") {
			transform.parent.GetComponent<hookablesMove> ()._canPush = true;
			Q.SetActive(true);
		}

	}
	void OnTriggerExit2D(Collider2D Coll){
		if (Coll.gameObject.tag == "Player") {
			transform.parent.GetComponent<hookablesMove> ()._canPush = false;
			Q.SetActive(false);
		}

	}

}
