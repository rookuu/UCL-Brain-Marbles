using UnityEngine;
using System.Collections;

public class marbleBehavior : MonoBehaviour {

    public bool isFake = false;
	GameObject levelController;
	public int scoreChange;
	int badClickScore;

    void Start ()
    {
		levelController = GameObject.FindGameObjectWithTag ("GameController");
		badClickScore = levelController.GetComponent<levelController> ().badClickScore;
    }

    public void catchMarble ()
    {
        Destroy(gameObject);
		levelController.GetComponent<levelController>().addScore (scoreChange);
	}

	public void badCatch ()
	{
		Destroy(gameObject);
		levelController.GetComponent<levelController> ().addScore (badClickScore);
	}

	public void updateSpeed(int speed) {
		if (GetComponent<moveRandomly> ().enabled == true) {
			GetComponent<moveRandomly> ().speedOfMarble = speed;
		}
	}
}
