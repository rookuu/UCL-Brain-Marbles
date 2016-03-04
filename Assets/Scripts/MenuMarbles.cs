using UnityEngine;
using System.Collections;

public class MenuMarbles : MonoBehaviour {

	int numberOfPathNodes;
	public int speedOfMarble = 10;

	// Use this for initialization
	void Start ()
	{
		numberOfPathNodes = 1000;
		Vector3[] path = new Vector3[numberOfPathNodes];

		for (int i = 0; i < numberOfPathNodes; i++)
		{
			path[i] = new Vector3(Random.Range(-10, 10), Random.Range(-6, 6), 0);
		}

		runThroughPath(path);
	}

	// Update is called once per frame
	void Update ()
	{

	}

	void runThroughPath(Vector3[] path)
	{
		iTween.MoveTo(gameObject, iTween.Hash("path", path, "speed", speedOfMarble, "easetype",  iTween.EaseType.linear, "oncomplete", "flyOffScreen"));
	}
		


}