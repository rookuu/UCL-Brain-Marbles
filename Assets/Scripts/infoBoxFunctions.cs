using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Data;
using Mono.Data.SqliteClient;

public class infoBoxFunctions : MonoBehaviour {
	string _dbName = "URI=file:brainmarbles.db";
	IDbConnection _conn;
	IDbCommand _cmd;

	public void closeBox() {
		gameObject.SetActive (false);
	}

	public void startGame() {
		GameObject.Find ("marbleController").GetComponent<marbleController> ().startGame ();
		gameObject.SetActive (false);
	}

	public void goTutorial() {
		SceneManager.LoadScene ("Tutorial");

	}

	public void noTutorial() {
		gameObject.SetActive (false);

		_conn = new SqliteConnection(_dbName);
		_cmd = _conn .CreateCommand();
		_conn .Open();

		globalData data = GameObject.Find ("GlobalData").GetComponent<globalData> ();
		_cmd.Parameters.Add(new SqliteParameter ("@userid", data.userID));

		_cmd.CommandText = "UPDATE `users` SET tutorial='skipped' WHERE userid=@userid;";

		_cmd.ExecuteNonQuery ();
		_conn.Close ();
	}
}
