using UnityEngine;
using System.Collections;

public class floatingobjects : MonoBehaviour {

	private float currentPosition;

	public float interval;

	public float maxDistance;

	public GameObject parent;

	// Use this for initialization
	void Start () {

		currentPosition = 0f;
	
	}
	
	// Update is called once per frame
	void Update () {
		if (parent.layer == 10) {
			if (currentPosition <= maxDistance) {

				currentPosition = currentPosition + (interval * Time.deltaTime);

				this.transform.localPosition = new Vector3 (0f, currentPosition, 0f);
		
			}
		}
		if(parent.layer != 10 && currentPosition >= 0f){

			currentPosition = Mathf.MoveTowards (currentPosition, 0f, (4.0f * interval * Time.deltaTime));

			this.transform.localPosition = new Vector3 (0f, currentPosition, 0f);

		}
	}
}
