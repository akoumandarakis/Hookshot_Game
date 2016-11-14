using UnityEngine;
using System.Collections;

public class LightColorPick : MonoBehaviour {
	public bool Red;
	public bool Green;
	public bool Blue;
	public bool LightBlue;
	public bool Purple;
	public bool White;
	SpriteRenderer Rend;
	// Use this for initialization
	void Start () {
		Rend = gameObject.GetComponent<SpriteRenderer> ();
		if (Red == true){
			Rend.color = new Color (.88f, 0.066f, 0.082f, 1.0f);

		}
		if (Green == true){
			Rend.color = new Color (.208f, 0.972f, 0.494f, 1.0f);

		}
		if (Blue == true){
			Rend.color = new Color (.09f, 0.074f, 0.776f, 1.0f);

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
	}

	// Update is called once per frame
	void Update () {
	
	}
}
