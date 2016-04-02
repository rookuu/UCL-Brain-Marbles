// Mutes-Unmutes the sound from this object each time the user presses m.
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class mute : MonoBehaviour
{
    AudioSource audio;
	public Toggle toggle;

	void Start() {
		audio = GameObject.Find ("GlobalData").GetComponent<AudioSource> ();
	}

    void Update()
    {
		if (Input.GetKeyDown (KeyCode.M))
		if (audio.mute) {
			toggle.isOn = false;
		} else {
			toggle.isOn = true;
		}
    }

	public void muteMusic()
	{
		if (audio.mute) {
			audio.mute = false;
		} else {
			audio.mute = true;
		}
	}
}