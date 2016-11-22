using UnityEngine;
using System.Collections;

public class HookablesAnimation : MonoBehaviour {

	public Animation anim;
	public float waitTime;

	// Use this for initialization
	void Start () {
		waitTime = 3f;
		anim = GetComponent<Animation> ();
		InvokeRepeating ("playGloss", 6f, waitTime);

	}
	void playGloss (){

		anim.Play ("hookable_glare_1");


	}
	// Update is called once per frame
	void Update () {
	
	}
}
