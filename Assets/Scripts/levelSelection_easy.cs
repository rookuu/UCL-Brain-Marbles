using UnityEngine;
using System.Collections;
using System.Data;
using Mono.Data.SqliteClient;
using UnityEngine.UI;

public class levelSelection_easy : MonoBehaviour {
	string _dbName = "URI=file:brainmarbles.db";
	IDbConnection _conn;
	IDbCommand _cmd;
	IDataReader _reader;

	public int numberOfLevels;
	bool found;
	public int lastCompleted;

	public Button level1;
	public Button level2;
	public Button level3;
	public Button level4;
	public Button level5;
	public Button level6;
	public Button level7;
	public Button level8;

	// Use this for initialization
	void Awake () {
		_conn = new SqliteConnection(_dbName);
		_cmd = _conn .CreateCommand();
		_conn.Open();

		globalData data = GameObject.Find ("GlobalData").GetComponent<globalData> ();
		_cmd.Parameters.Add(new SqliteParameter ("@userid", data.userID));

		for (int i = 1; i <= numberOfLevels; i++) {
			_cmd.CommandText = "SELECT * FROM `levelsessions` WHERE `userid`=@userid AND `stageid`=" + i + ";";
			_reader = _cmd.ExecuteReader ();
			found = false;

			while (_reader.Read ()) {
				if ((string)_reader ["status"] == "pass") {
					changeLevelStatus (i, "open");
					found = true;
					lastCompleted = i;
					break;
				}
			}

			if (found == false) {
				changeLevelStatus (i, "locked");
			}
		}

		if (lastCompleted != numberOfLevels) {
			changeLevelStatus (lastCompleted + 1, "open");
			Debug.Log ((lastCompleted + 1).ToString ());
		}
	}

	void changeLevelStatus(int stageid, string status) {
		if (stageid == 1) {
			changeButtonColour (level1, status);
		} else if (stageid == 2) {
			changeButtonColour (level2, status);
		} else if (stageid == 3) {
			changeButtonColour (level3, status);
		} else if (stageid == 4) {
			changeButtonColour (level4, status);
		} else if (stageid == 5) {
			changeButtonColour (level5, status);
		} else if (stageid == 6) {
			changeButtonColour (level6, status);
		} else if (stageid == 7) {
			changeButtonColour (level7, status);
		} else if (stageid == 8) {
			changeButtonColour (level8, status);
		}
	}

	void changeButtonColour(Button btn, string status) {
		if (status == "open") {
			btn.GetComponent<Button> ().interactable = true;
		} else if (status == "locked") {
			btn.GetComponent<Button> ().interactable = false;
		}
	}
}
