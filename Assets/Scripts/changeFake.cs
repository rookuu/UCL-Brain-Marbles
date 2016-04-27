using UnityEngine;
using System.Collections;

public class changeFake : MonoBehaviour {

	/* Marble Modifier Class: Makes a Marble alternate between being real and fake at random intervals */

	int numberOfPathNodes;
	public int speedOfMarble;
	public int xBoundary;
	public int yBoundary;
	public int maxNodes;
	public int minNodes;
	float time = 5.0f;
	marbleBehavior mb;
	int originalScoreChange;
	public int FakeScoreChange = -200;

	// Use this for initialization
	void Start ()
	{
		mb = GetComponent<marbleBehavior> ();

		numberOfPathNodes = Random.Range(minNodes, maxNodes);
		Vector3[] path = new Vector3[numberOfPathNodes];

		for (int i = 0; i < numberOfPathNodes; i++)
		{
			path[i] = new Vector3(Random.Range(-xBoundary, xBoundary), Random.Range(-yBoundary, yBoundary), 0);
		}

		runThroughPath(path);

		originalScoreChange = mb.scoreChange;
	}

	// Update is called once per frame
	void Update ()
	{
		time -= Time.deltaTime;

		if (time < 0) {
			iTween.ShakeScale (gameObject, iTween.Hash ("amount", new Vector3 (1, 1, 0), "time", 1, "oncomplete", "changeStatus"));
			time = (float)Random.Range(3,7);
		}

	}

	void runThroughPath(Vector3[] path)
	{
		iTween.MoveTo(gameObject, iTween.Hash("path", path, "speed", speedOfMarble, "easetype",  iTween.EaseType.linear, "oncomplete", "flyOffScreen"));
	}

	void flyOffScreen()
	{
		int rand = Random.Range(1, 4);
		Vector3 offScreenPos;

		if (rand == 1)
		{
			offScreenPos = new Vector3(Random.Range(-xBoundary, xBoundary), yBoundary + 10, 0);
		}
		else if (rand == 2)
		{
			offScreenPos = new Vector3(xBoundary + 10, Random.Range(-yBoundary, yBoundary), 0);
		}
		else if (rand == 3)
		{
			offScreenPos = new Vector3(Random.Range(-xBoundary, xBoundary), -yBoundary, 0);
		}
		else
		{
			offScreenPos = new Vector3(-xBoundary, Random.Range(-yBoundary, yBoundary), 0);
		}


		iTween.MoveTo(gameObject, iTween.Hash("position", offScreenPos, "speed", speedOfMarble, "easetype", iTween.EaseType.linear, "oncomplete", "destroyMarble"));
	}

	public void destroyMarble()
	{
		DestroyImmediate(gameObject);
	}

	public void changeStatus() 
	{
		if (mb.isFake == true) {
			gameObject. GetComponent<SpriteRenderer> ().color = Color.white;
			mb.isFake = false;
			mb.scoreChange = originalScoreChange;
		} else {
			gameObject.GetComponent<SpriteRenderer> ().color = new Color (0, 0, 0, 0.8f);
			mb.isFake = true;
			mb.scoreChange = FakeScoreChange;
		}
	}
}