using UnityEngine;
using System.Collections;

public class ZeroGSwitchScript : MonoBehaviour {

	public GameObject ZeroGArea;

	void OnTriggerEnter2D (Collider2D collider) 
	{
		HookShotScript hookshot = collider.GetComponent<HookShotScript> ();

		if (hookshot != null && ZeroGArea != null) 
		{
			if (ZeroGArea.layer == 0) 
			{
				ZeroGArea.layer = LayerMask.NameToLayer ("Trigger");
			} 
			else if (ZeroGArea.layer == 10) 
			{
				ZeroGArea.layer = LayerMask.NameToLayer ("Default");
			}

		}
	}
}
