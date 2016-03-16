using UnityEngine;
using System.Collections;
using System.Data;
using Mono.Data.SqliteClient;
using UnityEngine.UI;

public class levelSelection : MonoBehaviour {
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
					changeLevelStatus (i, "passed");
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
		}
	}

	void changeLevelStatus(int stageid, string status) {
		if (stageid == 1) {
			changeButtonColour (level1, status);
		} else if (stageid == 2) {
			changeButtonColour (level2, status);
		} else if (stageid == 3) {
			changeButtonColour (level3, status);
		} else {
			changeButtonColour (level4, status);
		}
	}

	void changeButtonColour(Button btn, string status) {
		if (status == "passed") {
			btn.GetComponent<Image> ().color = Color.green;
		} else if (status == "open") {
			btn.GetComponent<Image> ().color = Color.blue;
		} else {
			btn.GetComponent<Image> ().color = Color.red;
		}
	}
}
