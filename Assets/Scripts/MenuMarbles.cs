using UnityEngine;
using System.Collections;

/* Simpler version of the move randomly script with an infinite run cycle so the marbles will randomly move around behind the menus */

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
			path[i] = new Vector3(Random.Range(-20, 20), Random.Range(-12, 12), 0);
		}

		runThroughPath(path);
	}

	// Update is called once per frame
	void Update ()
	{

	}

	void runThroughPath(Vector3[] path)
	{
		iTween.MoveTo(gameObject, iTween.Hash("path", path, "speed", speedOfMarble, "easetype",  iTween.EaseType.linear, "looptype", iTween.LoopType.pingPong));
	}
		


}