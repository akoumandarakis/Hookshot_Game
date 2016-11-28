using UnityEngine;
using System.Collections;

public class hookablesMove : MonoBehaviour {
	public object _hookableObject;
	BoxCollider2D _buttonCollider;
	public bool _canPush = false;
	BoxCollider2D[] _buttonColliderList;
	GameObject Q;
	GameObject Right;
	GameObject Left;
	GameObject Crane;
	float direction;
	GameObject player;
	public float maxDistance;

	// Use this for initialization
	void Start () {
		 _buttonColliderList = gameObject.GetComponentsInChildren<BoxCollider2D> ();
		Q = transform.FindChild ("Q").gameObject;
		Right = transform.FindChild ("right arrow").gameObject;
		Left = transform.FindChild ("left arrow").gameObject;
		Crane = transform.FindChild ("Crane").gameObject;
		player = GameObject.FindGameObjectsWithTag("Player")[0];


	}

	// Update is called once per frame
	void Update () {
		if (_canPush == true) {
			direction = (player.transform.position.x) - (this.transform.position.x);
			Debug.Log (direction);
			if (direction > 0f){
				Right.SetActive (true);
				Left.SetActive (false);

			}
			if (direction <= 0f){
				Right.SetActive (false);
				Left.SetActive (true);

			}
			if (!Input.GetButton ("Button Press")) {
				Q.SetActive (true);
			}
			if (Input.GetButton ("Button Press") && Crane.transform.localPosition.x <= maxDistance && direction > 0f) {
				Q.SetActive (false);
				float currentLocation = Crane.transform.localPosition.x;
				float deltax = Mathf.MoveTowards(currentLocation, 3.0f, Time.deltaTime);
				Crane.transform.localPosition = new Vector3 (deltax, Crane.transform.localPosition.y, 0f);
			}
			if (Input.GetButton ("Button Press") && Crane.transform.localPosition.x >= -maxDistance && direction <= 0f) {
				Q.SetActive (false);
				float currentLocation = Crane.transform.localPosition.x;
				float deltax = Mathf.MoveTowards (currentLocation, -3.0f, Time.deltaTime);
				Crane.transform.localPosition = new Vector3 (deltax, Crane.transform.localPosition.y, 0f);
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
			Q.SetActive (false);
		}

	}

}
