using UnityEngine;
using System.Collections;

public class hookablesMove : MonoBehaviour {
	public object _hookableObject;
	BoxCollider2D _buttonCollider;
	public bool _canPush = false;
	BoxCollider2D[] _buttonColliderList;
	// Use this for initialization
	void Start () {
		 _buttonColliderList = gameObject.GetComponentsInChildren<BoxCollider2D> ();

	}

	// Update is called once per frame
	void Update () {
		if (_canPush == true) {
			if (Input.GetButton ("Button Press")) {
				Debug.Log ("push button");
			}
		
		}

	}

	void OnTriggerEnter2D(Collider2D Coll){
		if (Coll.gameObject.tag == "Player") {
			_canPush = true;

		}

	}
	void OnTriggerExit2D(Collider2D Coll){
		if (Coll.gameObject.tag == "Player") {
			_canPush = false;

		}

	}

}
