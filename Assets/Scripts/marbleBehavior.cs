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
			source.PlayOneShot (goodcatch, 1f); 
		} else {
			source.PlayOneShot (badcatch, 1f);
		}
		iTween.ScaleTo (gameObject, iTween.Hash ("time", 1, "scale", new Vector3 (0, 0, 0), "oncomplete", "destroyMarble"));
		levelController.GetComponent<levelController>().addScore (scoreChange);
	}

	public void badCatch ()
	{
		source.PlayOneShot (badcatch, 1f);
		Destroy(gameObject);
		levelController.GetComponent<levelController> ().addScore (badClickScore);
	}

	public void updateSpeed(int speed) {
		if (GetComponent<moveRandomly> ().enabled == true) {
			GetComponent<moveRandomly> ().speedOfMarble = speed;
		}
	}
}
