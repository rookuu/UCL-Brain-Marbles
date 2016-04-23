using UnityEngine;
using System.Collections;

public class marbleBehavior : MonoBehaviour {

    public bool isFake = false;
	GameObject levelController;
	public int scoreChange;
	int badClickScore;

	public AudioClip goodcatch, badcatch;
	AudioSource source;

    void Start ()
    {
		levelController = GameObject.FindGameObjectWithTag ("GameController");
		badClickScore = levelController.GetComponent<levelController> ().badClickScore;
		source = GetComponent<AudioSource>();
    }

	public void destroyMarble() {
		Destroy (gameObject);
	}

    public void catchMarble ()
	{
		if (isFake == false) {
			levelController.GetComponent<levelController> ().goodMarbles += 1;
			source.PlayOneShot (goodcatch, 1f);
			iTween.ScaleTo (gameObject, iTween.Hash ("time", 1, "scale", new Vector3 (0, 0, 0), "oncomplete", "destroyMarble"));
		} else {
			levelController.GetComponent<levelController> ().badMarbles += 1;
			source.PlayOneShot (badcatch, 1f);
			iTween.ShakePosition (gameObject, iTween.Hash ("amount", new Vector3 (1, 1, 0), "time", 1));
			iTween.ScaleTo (gameObject, iTween.Hash ("time", 1.5, "scale", new Vector3 (0, 0, 0), "oncomplete", "destroyMarble"));
		}

		if (isFake == true && scoreChange > 0) {
			levelController.GetComponent<levelController> ().addScore (-1 * scoreChange);
		} else {
			levelController.GetComponent<levelController>().addScore (scoreChange);

		}
	}

	public void badCatch ()
	{
		levelController.GetComponent<levelController> ().misses += 1;
		source.PlayOneShot (badcatch, 1f);
		iTween.ShakePosition (gameObject, iTween.Hash ("amount", new Vector3 (1, 1, 0), "time", 1));
		iTween.ScaleTo (gameObject, iTween.Hash ("time", 1.5, "scale", new Vector3 (0, 0, 0), "oncomplete", "destroyMarble"));
		levelController.GetComponent<levelController> ().addScore (badClickScore);
	}

	public void updateSpeed(int speed) {
		if (GetComponent<moveRandomly> ().enabled == true) {
			GetComponent<moveRandomly> ().speedOfMarble = speed;
		} else if (GetComponent<changeFake> ().enabled == true) {
			GetComponent<changeFake> ().speedOfMarble = speed;
		}
	}
}
