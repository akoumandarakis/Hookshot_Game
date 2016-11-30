using UnityEngine;
using System.Collections;

public class ReticleScript : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		UnityEngine.Cursor.visible = false;

		Vector3 mousePos = Input.mousePosition;
		Vector3 screenPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, transform.position.z - Camera.main.transform.position.z));
		this.transform.position = screenPos;
	
	}
}
