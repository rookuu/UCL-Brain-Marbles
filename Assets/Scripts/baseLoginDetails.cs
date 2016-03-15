using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class baseLoginDetails : MonoBehaviour {
	public InputField firstName;
	public InputField email;
	public InputField password;
	public InputField passwordVerify;

	public void registerAcc() {
		if (password.text == passwordVerify.text) {
			if (validateInfo()) {
				globalData data = GameObject.Find ("GlobalData").GetComponent<globalData> ();
				data.userName = firstName.text;
				data.userEmail = email.text;
				data.userPass = password.text;
				data.saveData ();
				SceneManager.LoadScene (2);
			}
		} else {
			Debug.Log ("Account Creation Failed: Passwords don't match!");
		}

	}

	bool validateInfo(){
		// VALIDATE
		return true;
	}
}
