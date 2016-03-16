using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Data;
using Mono.Data.SqliteClient;

public class baseLoginDetails : MonoBehaviour {
	public InputField firstName;
	public InputField email;
	public InputField password;
	public InputField passwordVerify;

	string _dbName = "URI=file:brainmarbles.db";
	IDbConnection _conn;
	IDbCommand _cmd;
	IDataReader _reader;

	public void registerAcc() {
		if (password.text == passwordVerify.text) {
			if (validateInfo() && checkExist()) {
				globalData data = GameObject.Find ("GlobalData").GetComponent<globalData> ();
				data.userName = firstName.text;
				data.userEmail = email.text;
				data.userPass = password.text;
				data.saveData ();
				SceneManager.LoadScene (2);
			}
		} else {
			Debug.Log ("Passwords don't match!");
		}

	}

	bool validateInfo(){
		if (firstName.text == "") {
			Debug.Log ("Please enter a valid name");
			return false;
		} else if (email.text.Length < 6 || email.text.Contains ("@") == false || email.text.Contains (".") == false) {
			Debug.Log ("Please enter a valid email address.");
			return false;
		} else if (password.text.Length < 6) {
			Debug.Log ("Please enter a longer password (6 or more characters).");
			return false;
		} else {
			Debug.Log ("Validation Sucessful");
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
			Debug.Log ("Email already exists!");
			return false;
		} else {
			return true;
		}

	}
}
