using UnityEngine;
using System.Collections;

public class LightColorPick : MonoBehaviour {
	public bool Red;
	public bool Green;
	public bool Blue;
	public bool LightBlue;
	public bool Purple;
	public bool White;
	public bool Orange;
	public bool Yellow;
	SpriteRenderer Rend;
	// Use this for initialization
	void Awake () {
		Rend = gameObject.GetComponent<SpriteRenderer> ();
		if (Red == true){
			Rend.color = new Color (.88f, 0.066f, 0.082f, 1.0f);

		}
		if (Green == true){
			Rend.color = new Color (.208f, 0.972f, 0.494f, 1.0f);

		}
		if (Blue == true){
			Rend.color = new Color (.0078f, 0.705f, 0.99f, 1.0f);

		}
		if (LightBlue == true){
			Rend.color = new Color (.157f, 0.898f, 0.925f, 1.0f);

		}
		if (Purple == true){
			Rend.color = new Color (.694f, 0.129f, 0.878f, 1.0f);

		}
		if (White == true){
			Rend.color = new Color (1.0f, 1.0f, 1.0f, 1.0f);

		}
		if (Orange == true){
			Rend.color = new Color (.99f, .474f, .007f, 1.0f);

		}
		if (Yellow == true){
			Rend.color = new Color (.99f, .96f, .003f, 1.0f);

		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
