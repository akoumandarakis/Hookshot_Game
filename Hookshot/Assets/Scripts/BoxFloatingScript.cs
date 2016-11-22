using UnityEngine;
using System.Collections;

public class BoxFloatingScript : MonoBehaviour {

	private SoldierMoveScript floatingScript;
	private MoveScript fallScript;
	private Vector3 startPos;
	 
	public GameObject parent;

	void Start()
	{
		floatingScript = this.gameObject.GetComponent<SoldierMoveScript> ();
		fallScript = this.gameObject.GetComponent<MoveScript> ();
		startPos = this.transform.position;
	}

	// Update is called once per frame
	void Update () {
		if (parent.layer == 10) {
			floatingScript.enabled = true;
			fallScript.enabled = false;
		} else {
			floatingScript.enabled = false;
			floatingScript.direction = new Vector2 (0, 1);
			fallScript.enabled = true;
		}

		if (fallScript.enabled && this.transform.position.y <= startPos.y) 
		{
			fallScript.enabled = false;
			this.transform.position = startPos;
		}
	
	}
}
