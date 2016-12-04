using UnityEngine;
using System.Collections;

public class floatingobjects : MonoBehaviour {

	private float currentPosition;

	public float interval;

	public float maxDistance;


	// Use this for initialization
	void Start () {

		currentPosition = 0f;
	
	}
	
	// Update is called once per frame
	void Update () {
		//if (zeroG) {
			if (currentPosition <= maxDistance) {

				currentPosition = Mathf.MoveTowards (currentPosition, maxDistance, (interval * Time.deltaTime));

				this.transform.localPosition = new Vector3 (0f, currentPosition, 0f);
		
			}
		//}
		//if(notzeroG && currentPosition >= 0f){

			//currentPosition = Mathf.MoveTowards (currentPosition, 0f, (interval * Time.deltaTime));

			//this.transform.localPosition = new Vector3 (0f, currentPosition, 0f);

		//}
	}
}
