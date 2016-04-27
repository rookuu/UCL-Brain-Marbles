using UnityEngine;
using System.Collections;

/* Contains a modified version of the marble behaviour class for the tutorial */

public class tutorialMarble : MonoBehaviour {

	public int numberOfPathNodes;
	public int speedOfMarble;
	public int xBoundary;
	public int yBoundary;

	AudioSource source;
	public AudioClip goodcatch;
	public AudioClip badcatch;

	Vector3[] path;


	void Start() {
		source = GetComponent<AudioSource> ();

		path = new Vector3[numberOfPathNodes];

		for (int i = 0; i < numberOfPathNodes; i++)
		{
			path[i] = new Vector3(Random.Range(-xBoundary, xBoundary), Random.Range(-yBoundary, yBoundary), 0);
		}
	}

	void startMoving() {
		iTween.MoveTo (gameObject, iTween.Hash ("path", path, "speed", speedOfMarble, "easetype", iTween.EaseType.linear, "oncomplete", "startMoving"));
	}

	public void destroyMarble() {
		Destroy (gameObject);
		GameObject.Find ("tutorialController").GetComponent<tutorialController> ().clicked = true;
	}

	public void catchMarble ()
	{

		source.PlayOneShot (goodcatch, 1f);
		iTween.ScaleTo (gameObject, iTween.Hash ("time", 1, "scale", new Vector3 (0, 0, 0), "oncomplete", "destroyMarble"));

	}

	public void badCatch ()
	{
		source.PlayOneShot (badcatch, 1f);
		iTween.ShakeScale (gameObject, iTween.Hash ("amount", new Vector3 (1, 1, 0), "time", 1));
	}

}
