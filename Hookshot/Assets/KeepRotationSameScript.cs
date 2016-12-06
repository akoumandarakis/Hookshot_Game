using UnityEngine;
using System.Collections;

public class KeepRotationSameScript : MonoBehaviour {

	private Vector3 startRotation;

	// Use this for initialization
	void Start () {
		startRotation = this.gameObject.transform.eulerAngles;
	
	}
	
	// Update is called once per frame
	void Update () {
		this.gameObject.transform.eulerAngles = startRotation;
	
	}
}
