using UnityEngine;
using System.Collections;

public class globalData : MonoBehaviour {

	public static globalData instance;
	public bool loggedIn = false;

	public string userName = null;
	public string userEmail = null;
	public string userPass = null;

	void Start () {
		loggedIn = globalData.instance.loggedIn;

		userName = globalData.instance.userName;
		userEmail = globalData.instance.userEmail;
		userPass = globalData.instance.userPass;
	}
	
	// Update is called once per frame
	void Awake () {
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

		globalData.instance.userName = userName;
		globalData.instance.userEmail = userEmail;
		globalData.instance.userPass = userPass;
	}
}
