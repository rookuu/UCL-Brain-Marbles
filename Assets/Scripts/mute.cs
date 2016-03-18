// Mutes-Unmutes the sound from this object each time the user presses m.
using UnityEngine;
using System.Collections;

public class mute : MonoBehaviour
{
    AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
            if (audio.mute)
                audio.mute = false;
            else
                audio.mute = true;

    }
}