using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class levelController : MonoBehaviour {
	int score = 0;
	public int scoreTarget;
	float time = 0;
	public float timeLimit;

	public int badClickScore;

	public Text textTime;
	public Text textScore;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (time > timeLimit) {
			textTime.text = "GAME OVER!";
			GameObject.Find ("marbleController").GetComponent<marbleController> ().isRunning = false;
			killAllMarbles ();

		} else {
			time += Time.deltaTime;
			textTime.text = ((int)(timeLimit - time)).ToString ();
		}
			
		if (score > scoreTarget) {
			textScore.text = "GAME WIN!";
		} else {
			textScore.text = ((int)score).ToString();
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
}
