using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Data;
using Mono.Data.SqliteClient;
using UnityEngine.SceneManagement;

public class levelController : MonoBehaviour {
	int score = 0;
	public int scoreTarget;
	float time = 0;
	public float timeLimit;

	public int badClickScore;

	public Text textStatus;

	public int goodMarbles = 0, badMarbles = 0, misses = 0;

	string _dbName = "URI=file:brainmarbles.db";
	IDbConnection _conn;
	IDbCommand _cmd;

	public GameObject levelwin, levellose;
	public Text num1, num2, score1, score2;

	public Text levelInfo, leveltitle;
	public GameObject marbleUI, infoUI, smallMarbleUI, horizLayout, infoLayout, smallLayout;

	public AudioClip gamebgm;

	// Use this for initialization
	void Start () {
		displayInfoBox ();

		GameObject.Find ("GlobalData").GetComponent<globalData> ().changeSong (gamebgm);
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.Find ("marbleController").GetComponent<marbleController> ().isRunning == true) {
			if (time > timeLimit) {
				GameObject.Find ("marbleController").GetComponent<marbleController> ().isRunning = false;
				killAllMarbles ();

				if (score > scoreTarget) {
					saveSession ("pass");
					levelwin.SetActive (true);
					num2.GetComponent<Text> ().text = "Level " + GameObject.Find ("GlobalData").GetComponent<globalData> ().btnText;
					score2.GetComponent<Text> ().text = "Score: " + score.ToString();
				} else {
					saveSession ("fail");
					levellose.SetActive (true);
					num1.GetComponent<Text> ().text = "Level " + GameObject.Find ("GlobalData").GetComponent<globalData> ().btnText;
					score1.GetComponent<Text> ().text = "Score: " + score.ToString();
				}

			} else {
				time += Time.deltaTime;
				textStatus.text = "Level " + GameObject.Find("loadGameData").GetComponent<loadLevelData>().stageid.ToString() + " • Time: " + ((int)(timeLimit - time)).ToString () + "s • Score: " + score.ToString() + " • Target: " + scoreTarget.ToString();
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
		_cmd.Parameters.Add(new SqliteParameter ("@good", goodMarbles));
		_cmd.Parameters.Add(new SqliteParameter ("@bad", badMarbles));
		_cmd.Parameters.Add(new SqliteParameter ("@miss", misses));
		_cmd.Parameters.Add(new SqliteParameter ("@status", status));
		_cmd.Parameters.Add(new SqliteParameter ("@unixtime", (int)(System.DateTime.UtcNow.Subtract(new System.DateTime(1970, 1, 1))).TotalSeconds));
		_cmd.Parameters.Add (new SqliteParameter ("@bgm", (!GameObject.Find ("GlobalData").GetComponent<AudioSource> ().mute).ToString ()));
		_cmd.Parameters.Add (new SqliteParameter ("@screenx", Screen.width));
		_cmd.Parameters.Add (new SqliteParameter ("@screeny", Screen.height));
			
		_cmd.CommandText = "INSERT INTO `levelsessions` (stageid, levelid, userid, score, goodmarbles, fakemarbles" +
			", misses, status, unixtime, bgm, screenx, screeny) VALUES (@stageid, @levelid, @userid, @score, @good, @bad, @miss, @status, @unixtime, @bgm, @screenx, @screeny);";
		_cmd.ExecuteNonQuery ();
		_conn.Close ();
	}

	void displayInfoBox() {
		textStatus.text = "Level " + GameObject.Find("loadGameData").GetComponent<loadLevelData>().stageid.ToString() + " • Time: " + ((int)(timeLimit - time)).ToString () + "s • Score: " + score.ToString() + " • Target: " + scoreTarget.ToString();
		leveltitle.text = "Level " + GameObject.Find ("loadGameData").GetComponent<loadLevelData> ().stageid.ToString();
		levelInfo.text = "You have " + timeLimit.ToString () + " seconds to collect " + scoreTarget.ToString () + " points. Misses will deduct " + badClickScore.ToString () + " points.";

		GameObject newUIMarble;
		GameObject newUISmall;
		GameObject newUIInfo;

		marbleController mc = GameObject.Find ("marbleController").GetComponent<marbleController> ();
		int uniqueMarbles = mc.uniqueMarbles;

		if (uniqueMarbles >= 1) {
			marbleUI.GetComponent<Image> ().color = statusColour (mc.fake1);
			marbleUI.transform.GetChild (0).GetComponent<Image> ().sprite = mc.sprite1;
			newUIMarble = Instantiate (marbleUI);
			newUIMarble.transform.SetParent (horizLayout.transform, false);

			infoUI.GetComponent<Text> ().text = mc.scoreChange1.ToString();
			newUIInfo = Instantiate (infoUI);
			newUIInfo.transform.SetParent (infoLayout.transform, false);

			smallMarbleUI.GetComponent<Image> ().color = statusColour (mc.fake1);
			smallMarbleUI.transform.GetChild (0).GetComponent<Image> ().sprite = mc.sprite1;
			newUISmall = Instantiate (smallMarbleUI);
			newUISmall.transform.SetParent (smallLayout.transform, false);
		} 
		if (uniqueMarbles >= 2) {
			marbleUI.GetComponent<Image> ().color = statusColour (mc.fake2);
			marbleUI.transform.GetChild (0).GetComponent<Image> ().sprite = mc.sprite2;
			newUIMarble = Instantiate (marbleUI);
			newUIMarble.transform.SetParent (horizLayout.transform, false);

			infoUI.GetComponent<Text> ().text = mc.scoreChange2.ToString();
			newUIInfo = Instantiate (infoUI);
			newUIInfo.transform.SetParent (infoLayout.transform, false);

			smallMarbleUI.GetComponent<Image> ().color = statusColour (mc.fake2);
			smallMarbleUI.transform.GetChild (0).GetComponent<Image> ().sprite = mc.sprite2;
			newUISmall = Instantiate (smallMarbleUI);
			newUISmall.transform.SetParent (smallLayout.transform, false);
		} 
		if (uniqueMarbles >= 3) {
			marbleUI.GetComponent<Image> ().color = statusColour (mc.fake3);
			marbleUI.transform.GetChild (0).GetComponent<Image> ().sprite = mc.sprite3;
			newUIMarble = Instantiate (marbleUI);
			newUIMarble.transform.SetParent (horizLayout.transform, false);

			infoUI.GetComponent<Text> ().text = mc.scoreChange3.ToString();
			newUIInfo = Instantiate (infoUI);
			newUIInfo.transform.SetParent (infoLayout.transform, false);

			smallMarbleUI.GetComponent<Image> ().color = statusColour (mc.fake3);
			smallMarbleUI.transform.GetChild (0).GetComponent<Image> ().sprite = mc.sprite3;
			newUISmall = Instantiate (smallMarbleUI);
			newUISmall.transform.SetParent (smallLayout.transform, false);
		}
		if (uniqueMarbles >= 4) {
			marbleUI.GetComponent<Image> ().color = statusColour (mc.fake4);
			marbleUI.transform.GetChild (0).GetComponent<Image> ().sprite = mc.sprite4;
			newUIMarble = Instantiate (marbleUI);
			newUIMarble.transform.SetParent (horizLayout.transform, false);

			infoUI.GetComponent<Text> ().text = mc.scoreChange4.ToString();
			newUIInfo = Instantiate (infoUI);
			newUIInfo.transform.SetParent (infoLayout.transform, false);

			smallMarbleUI.GetComponent<Image> ().color = statusColour (mc.fake4);
			smallMarbleUI.transform.GetChild (0).GetComponent<Image> ().sprite = mc.sprite4;
			newUISmall = Instantiate (smallMarbleUI);
			newUISmall.transform.SetParent (smallLayout.transform, false);
		}
		if (uniqueMarbles >= 5) {
			marbleUI.GetComponent<Image> ().color = statusColour (mc.fake5);
			marbleUI.transform.GetChild (0).GetComponent<Image> ().sprite = mc.sprite5;
			newUIMarble = Instantiate (marbleUI);
			newUIMarble.transform.SetParent (horizLayout.transform, false);

			infoUI.GetComponent<Text> ().text = mc.scoreChange5.ToString();
			newUIInfo = Instantiate (infoUI);
			newUIInfo.transform.SetParent (infoLayout.transform, false);

			smallMarbleUI.GetComponent<Image> ().color = statusColour (mc.fake5);
			smallMarbleUI.transform.GetChild (0).GetComponent<Image> ().sprite = mc.sprite5;
			newUISmall = Instantiate (smallMarbleUI);
			newUISmall.transform.SetParent (smallLayout.transform, false);
		}
		if (uniqueMarbles >= 6) {
			marbleUI.GetComponent<Image> ().color = statusColour (mc.fake6);
			marbleUI.transform.GetChild (0).GetComponent<Image> ().sprite = mc.sprite6;
			newUIMarble = Instantiate (marbleUI);
			newUIMarble.transform.SetParent (horizLayout.transform, false);

			infoUI.GetComponent<Text> ().text = mc.scoreChange6.ToString();
			newUIInfo = Instantiate (infoUI);
			newUIInfo.transform.SetParent (infoLayout.transform, false);

			smallMarbleUI.GetComponent<Image> ().color = statusColour (mc.fake6);
			smallMarbleUI.transform.GetChild (0).GetComponent<Image> ().sprite = mc.sprite6;
			newUISmall = Instantiate (smallMarbleUI);
			newUISmall.transform.SetParent (smallLayout.transform, false);
		}

		LayoutRebuilder.MarkLayoutForRebuild (horizLayout.transform as RectTransform);
		LayoutRebuilder.MarkLayoutForRebuild (infoLayout.transform as RectTransform);
		LayoutRebuilder.MarkLayoutForRebuild (smallLayout.transform as RectTransform);

	}

	Color statusColour(bool isFake) {
		if (isFake == true) {
			return Color.red;
		} else {
			return Color.green;
		}
	}
}


