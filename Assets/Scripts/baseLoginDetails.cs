using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Data;
using Mono.Data.SqliteClient;

public class baseLoginDetails : MonoBehaviour {
	public InputField firstName;
	public InputField lastName;
	public InputField email;
	public InputField password;
	public InputField passwordVerify;

	string _dbName = "URI=file:brainmarbles.db";
	IDbConnection _conn;
	IDbCommand _cmd;
	IDataReader _reader;

	public GameObject infobox;
	public Text message;

	public void registerAcc() {
		if (password.text == passwordVerify.text) {
			if (validateInfo() && checkExist()) {
				globalData data = GameObject.Find ("GlobalData").GetComponent<globalData> ();
				data.userName = firstName.text;
				data.userLast = lastName.text;
				data.userEmail = email.text;
				data.userPass = password.text;
				data.saveData ();
				SceneManager.LoadScene (2);
			}
		} else {
			displayMessage ("Error: Passwords don't match!");
		}

	}

	bool validateInfo(){
		if (firstName.text == "" || lastName.text == "") {
			displayMessage ("Error: Name field can't be left blank");
			return false;
		} else if (email.text.Length < 6 || email.text.Contains ("@") == false || email.text.Contains (".") == false) {
			displayMessage ("Error: Email isn't valid, ensure it's in the correct form.");
			return false;
		} else if (password.text.Length < 6) {
			displayMessage ("Error: Password must be longer than 6 characters");
			return false;
		} else {
			return true;
		}
	}

	bool checkExist(){
		_conn = new SqliteConnection(_dbName);
		_cmd = _conn .CreateCommand();
		_conn.Open();

		_cmd.Parameters.Add(new SqliteParameter ("@email", email.text));
		_cmd.CommandText = "SELECT `userid` FROM `users` WHERE `email`=@email";
		_reader = _cmd.ExecuteReader ();

		if (_reader.Read ()) {
			displayMessage ("Error: Email is already registered");
			return false;
		} else {
			return true;
		}

	}

	private void displayMessage(string msg) {
		infobox.SetActive (true);
		message.text = msg;
	}
}
