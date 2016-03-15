using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class login : MonoBehaviour {
	public Text email;
	public Text password;

	public void loginUser () {
		//Validation(email);
		//Validation(password);

		//Authenticate(email, password);

		globalData data = GameObject.Find ("GlobalData").GetComponent<globalData> ();
		data.loggedIn = true;
		data.userEmail = email.text;
		data.saveData ();
		SceneManager.LoadScene (5);

	}
}
