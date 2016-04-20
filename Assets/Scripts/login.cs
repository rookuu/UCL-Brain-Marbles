using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Data;
using Mono.Data.SqliteClient;

public class login : MonoBehaviour {
	string _dbName = "URI=file:brainmarbles.db";
	IDbConnection _conn;
	IDbCommand _cmd;
	IDataReader _reader;


	public InputField email;
	public InputField password;

	public GameObject infobox;
	public Text message;

	public void loginUser () {
		//Validation(email);
		//Validation(password);

		_conn = new SqliteConnection(_dbName);
		_cmd = _conn .CreateCommand();
		_conn.Open();

		_cmd.Parameters.Add(new SqliteParameter ("@email", email.text));

		_cmd.CommandText = "SELECT `passwd` FROM `users` WHERE `email`=@email;";
		_reader = _cmd.ExecuteReader ();

		if (_reader.Read ()) {
			if (BCrypt.CheckPassword (password.text + "r~BV2$J", (string)_reader ["passwd"])) {
				_cmd.CommandText = "SELECT * FROM `users` WHERE `email`=@email;";
				_reader = _cmd.ExecuteReader ();

				if (_reader.Read ()) {
					if (_reader ["tutorial"].ToString() == "admin") {
						SceneManager.LoadScene ("admin_panel");
					} else {
						globalData data = GameObject.Find ("GlobalData").GetComponent<globalData> ();
						data.loggedIn = true;
						data.userID = (int)_reader ["userid"];
						data.userEmail = (string)_reader ["email"];
						data.userName = (string)_reader ["firstname"];
						data.userPass = null;
						data.saveData ();
						SceneManager.LoadScene (4);
					}
				}
			} else {
				displayMessage ("Error: Email or Password is incorrect, please check and try again.");
			}
		} else {
			displayMessage ("Error: Email or Password is incorrect, please check and try again.");
		}

		_conn.Close ();
	}

	private void displayMessage(string msg) {
		infobox.SetActive (true);
		message.text = msg;
	}
}
