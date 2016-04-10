using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class globalData : MonoBehaviour {

	public static globalData instance;

	public bool loggedIn = false;

	public int userID = 0;
	public string userName = null;
	public string userLast = null;
	public string userEmail = null;
	public string userPass = null;

	public string btnText = null;

	public AudioClip menubgm, gamebgm;

	void Start () {
		loggedIn = globalData.instance.loggedIn;

		userID = globalData.instance.userID;
		userName = globalData.instance.userName;
		userLast = globalData.instance.userLast;
		userEmail = globalData.instance.userEmail;
		userPass = globalData.instance.userPass;

		btnText = globalData.instance.btnText;
	}
	
	void Awake () {
		AudioSource source = GetComponent<AudioSource> ();
	    source.clip = menubgm;
		source.Play ();

		if (instance == null) {
			DontDestroyOnLoad (gameObject);
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
	}

	public void saveData()
	{
		globalData.instance.loggedIn = loggedIn;

		globalData.instance.userID = userID;
		globalData.instance.userName = userName;
		globalData.instance.userLast = userLast;
		globalData.instance.userEmail = userEmail;
		globalData.instance.userPass = userPass;

		globalData.instance.btnText = btnText;
	}

	public void changeSong(AudioClip clip) {
		AudioSource source = GetComponent<AudioSource> ();
		source.clip = clip;
		source.Play ();
	}
}
