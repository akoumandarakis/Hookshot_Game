using UnityEngine;
using System.Collections;

public class MusicPlayerScript : MonoBehaviour {

	public AudioClip loopClip;

	// Use this for initialization
	IEnumerator Start () {
		AudioSource audio = GetComponent<AudioSource>();

        audio.ignoreListenerVolume = true;

		audio.Play();
		yield return new WaitForSeconds(audio.clip.length);
		if (loopClip != null) {
			audio.clip = loopClip;
			audio.Play ();
		}
	}
}
