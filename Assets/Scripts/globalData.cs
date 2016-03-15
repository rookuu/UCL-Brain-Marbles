using UnityEngine;
using System.Collections;

public class globalData : MonoBehaviour {

	public static globalData instance;
	public bool loggedIn = false;
	public string userEmail = null;

	void Start () {
		loggedIn = globalData.instance.loggedIn;
		userEmail = globalData.instance.userEmail;
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
		globalData.instance.userEmail = userEmail;
	}
}
