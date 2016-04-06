using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Data;
using Mono.Data.SqliteClient;

public class tutorialController : MonoBehaviour {
	string _dbName = "URI=file:brainmarbles.db";
	IDbConnection _conn;
	IDbCommand _cmd;

	public Text dialogMessage;
	public GameObject dialog;
	public GameObject dialogBtn;
	public GameObject arrow;

	Button btn;

	public Text statusMessage;

	public int stage = 0;
	public bool clicked = false;

	public GameObject realMarble1;
	public AudioClip gamebgm;

	public GameObject gameover;
	public Text overTitle;
	public Text overStatus;

	// Use this for initialization
	void Start () {
		btn = dialogBtn.GetComponent<Button> ();
		GameObject.Find ("GlobalData").GetComponent<globalData> ().changeSong (gamebgm);
	}
	
	// Update is called once per frame
	void Update () {
		if (stage == 0) {
			btn.onClick.AddListener (delegate {
				nextStageNoClose (1);
			});
			stage = -1;
			dialog.SetActive (true);
			dialogMessage.text = "Welcome to Brain Marbles, I'll be showing you the basic concepts. \n\nClick the [Next] button to advance.";
			statusMessage.text = "Tutorial 1 • Basic Concepts • Click [Next]";
		} else if (stage == 1) {
			btn.onClick.AddListener (delegate {
				nextStageNoClose (2);
			});
			stage = -1;
			dialog.SetActive (true);
			dialogMessage.text = "Firstly, to pause the game push [Esc] and to mute the music click the music icon in the top right (or push [m]).\n\nClick the [Next] button to advance.";
			arrow.SetActive (true);
			arrow.transform.localPosition = new Vector3 (372, 124, 0);
			arrow.transform.localEulerAngles = new Vector3 (0, 0, 70);
		} else if (stage == 2) {
			btn.onClick.AddListener (delegate {
				nextStage (3);
			});
			stage = -1;
			dialog.SetActive (true);
			dialogMessage.text = "In the top-left corner you'll see two different types of marbles, the green outline is a real marble (these will give you points) the red outline is a fake marbles (these will deduct points).\n\nClick the [Next] button to advance.";
			arrow.SetActive (true);
			arrow.transform.localPosition = new Vector3 (-358, 119, 0);
			arrow.transform.localEulerAngles = new Vector3 (0, 0, 90);
		} else if (stage == 3) {
			stage = -1;
			realMarble1.SendMessage ("startMoving");
			Invoke ("goNext4", 5f);
		} else if (stage == 4) {
			btn.onClick.AddListener (delegate {
				nextStage (5);
			});
			stage = -1;
			dialog.SetActive (true);
			dialogMessage.text = "Click on the real Marble to capture it. However, if you miss points are deducted and the marble is destroyed!\n\nClick the [Next] button to advance.";
			statusMessage.text = "Tutorial 1 • Basic Concepts • Click on the Marble to capture it!";
			clicked = false;
			GameObject.Find ("mousePointer").GetComponent<Tutorial_mousePointer> ().ready = true;
		} else if (stage == 5) {
			if (clicked == true) {
				btn.onClick.AddListener (delegate {
					//SceneManager.LoadScene("Level_Beginner");
					nextStage (6);
				});
				stage = -1;
				dialog.SetActive (true);
				dialogMessage.text = "Congratulations, you just gained points! The different real and fake marbles will be shown to you before the level starts.\nThat concludes this tutorial, more features will be explained once they're unlocked!";
				statusMessage.text = "Tutorial 1 • Basic Concepts • Tutorial Complete!";
			}
		} else if (stage == 6) {
			_conn = new SqliteConnection(_dbName);
			_cmd = _conn .CreateCommand();
			_conn .Open();

			globalData data = GameObject.Find ("GlobalData").GetComponent<globalData> ();
			_cmd.Parameters.Add(new SqliteParameter ("@userid", data.userID));

			_cmd.CommandText = "UPDATE `users` SET tutorial='completed' WHERE userid=@userid;";

			_cmd.ExecuteNonQuery ();
			_conn.Close ();

			stage = -1;
			gameover.SetActive (true);
			overTitle.text = "Tutorial 1";
			overStatus.text = "You've completed Tutorial 1! Click below to choose a level.";
		}
	}

	public void nextStageNoClose(int next) {
		stage = next;
		btn.onClick.RemoveAllListeners ();
	}

	public void nextStage(int next) {
		stage = next;
		dialog.SetActive (false);
		arrow.SetActive (false);
		btn.onClick.RemoveAllListeners ();
	}

	void goNext4() {
		nextStage (4);
	}


}
