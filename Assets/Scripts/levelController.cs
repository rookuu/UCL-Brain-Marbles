using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Data;
using Mono.Data.SqliteClient;

public class levelController : MonoBehaviour {
	int score = 0;
	public int scoreTarget;
	float time = 0;
	public float timeLimit;

	public int badClickScore;

	public Text textTime;
	public Text textScore;

	string _dbName = "URI=file:brainmarbles.db";
	IDbConnection _conn;
	IDbCommand _cmd;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.Find ("marbleController").GetComponent<marbleController> ().isRunning == true) {
			if (time > timeLimit) {
				textTime.text = "GAME OVER!";
				GameObject.Find ("marbleController").GetComponent<marbleController> ().isRunning = false;
				killAllMarbles ();
				saveSession ("fail");

			} else {
				time += Time.deltaTime;
				textTime.text = ((int)(timeLimit - time)).ToString ();
			}
				
			if (score > scoreTarget) {
				textScore.text = "GAME WIN!";
				GameObject.Find ("marbleController").GetComponent<marbleController> ().isRunning = false;
				killAllMarbles ();
				saveSession ("pass");
			} else {
				textScore.text = ((int)score).ToString ();
			}
		}
	}

	public void addScore(int x) {
		score += x;
	}

	public void killAllMarbles() {
		GameObject[] marbles = GameObject.FindGameObjectsWithTag ("Marble");
		for (int i = 0; i < marbles.Length; i++) {
			DestroyObject (marbles [i]);
		}
	}

	private void saveSession(string status) {
		_conn = new SqliteConnection(_dbName);
		_cmd = _conn .CreateCommand();
		_conn .Open();

		globalData data = GameObject.Find ("GlobalData").GetComponent<globalData> ();

		_cmd.Parameters.Add(new SqliteParameter ("@stageid", GameObject.Find("loadGameData").GetComponent<loadLevelData>().stageid));
		_cmd.Parameters.Add(new SqliteParameter ("@levelid", GameObject.Find("loadGameData").GetComponent<loadLevelData>().levelid));
		_cmd.Parameters.Add(new SqliteParameter ("@userid", data.userID));
		_cmd.Parameters.Add(new SqliteParameter ("@score", score));
		_cmd.Parameters.Add(new SqliteParameter ("@timeleft", (int)(timeLimit - time)));
		_cmd.Parameters.Add(new SqliteParameter ("@status", status));
			
		_cmd.CommandText = "INSERT INTO `levelsessions` (stageid, levelid, userid, score, timeleft, status) VALUES (@stageid, @levelid, @userid, @score, @timeleft, @status);";
		_cmd.ExecuteNonQuery ();
		_conn.Close ();
	}
}
