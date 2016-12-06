using UnityEngine;
using System.Collections;

public class ZeroGSwitchScript : MonoBehaviour {

	public GameObject ZeroGArea;

	private bool canSwitch;

	private HookShotScript hookshot;
	private PlayerScript player;
	private AnimationController2D anim;
	private int switchState = 0;

	void Start()
	{
		anim = this.gameObject.GetComponent<AnimationController2D>();
	}

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
					anim.setAnimation ("Zero G off to on");
				} else if (ZeroGArea.layer == 10) {
					anim.setAnimation ("zero g on to off");
					ZeroGArea.layer = LayerMask.NameToLayer ("Default");
				}
			}
		} else if (hookshot != null && (ZeroGArea != null && canSwitch)) {
			if (hookshot.transform.parent == null || hookshot.transform.parent == this.gameObject) {
				if (ZeroGArea.layer == 0) {
					anim.setAnimation ("Zero G off to on");
					ZeroGArea.layer = LayerMask.NameToLayer ("Trigger");
					canSwitch = false;
				} else if (ZeroGArea.layer == 10) {
					anim.setAnimation ("zero g on to off");
					ZeroGArea.layer = LayerMask.NameToLayer ("Default");
					canSwitch = false;
				}
			}
		}
			
	}
}
