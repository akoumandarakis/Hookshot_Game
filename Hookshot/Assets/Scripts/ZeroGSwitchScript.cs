using UnityEngine;
using System.Collections;

public class ZeroGSwitchScript : MonoBehaviour {

	public GameObject ZeroGArea;

	private bool canSwitch;

	private HookShotScript hookshot;
	private PlayerScript player;

	void OnTriggerEnter2D (Collider2D collider)
	{
		hookshot = collider.GetComponent<HookShotScript> ();
		player = collider.GetComponent<PlayerScript> ();
		canSwitch = true;
	}

	void OnTriggerExit2D(Collider2D collider)
	{
		hookshot = null;
		player = null;
		canSwitch = false;
	}

	void Update()
	{
		if (player != null && (ZeroGArea != null && canSwitch)) {
			if (Input.GetButtonDown ("Button Press")) {
				if (ZeroGArea.layer == 0) {
					ZeroGArea.layer = LayerMask.NameToLayer ("Trigger");
				} else if (ZeroGArea.layer == 10) {
					ZeroGArea.layer = LayerMask.NameToLayer ("Default");
				}
			}
		} else if (hookshot != null && (ZeroGArea != null && canSwitch)) {
			if (hookshot.transform.parent == null || hookshot.transform.parent == this.gameObject) {
				if (ZeroGArea.layer == 0) {
					ZeroGArea.layer = LayerMask.NameToLayer ("Trigger");
					canSwitch = false;
				} else if (ZeroGArea.layer == 10) {
					ZeroGArea.layer = LayerMask.NameToLayer ("Default");
					canSwitch = false;
				}
			}
		}
			
	}
}
