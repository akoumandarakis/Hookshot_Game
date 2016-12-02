using UnityEngine;
using System.Collections;

public class ZeroGSwitchScript : MonoBehaviour {

	public GameObject ZeroGArea;

	private bool canSwitch;

	private HookShotScript hookshot;

	void OnTriggerEnter2D (Collider2D collider)
	{
		hookshot = collider.GetComponent<HookShotScript> ();
		canSwitch = true;
	}

	void OnTriggerExit2D(Collider2D collider)
	{
		hookshot = null;
		canSwitch = false;
	}

	void Update()
	{
		if (hookshot != null && ZeroGArea != null && canSwitch) 
		{
			if (Input.GetAxis ("Down") > 0  || hookshot.gameObject.transform.parent == null) 
			{
				if (ZeroGArea.layer == 0) 
				{
					ZeroGArea.layer = LayerMask.NameToLayer ("Trigger");
				} 
				else if (ZeroGArea.layer == 10) 
				{
					ZeroGArea.layer = LayerMask.NameToLayer ("Default");
				}
				canSwitch = false;
			}
		}
	}
}
