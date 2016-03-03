﻿using UnityEngine;
using System.Collections;

public class marbleController : MonoBehaviour {
    public int x;
    public int percentOfFake;
    public GameObject Marble1;
    private GameObject[] noOfMarbles;
    private bool hasRun = false;
	public int xBoundary, yBoundary;

	// Use this for initialization
	void Start () {
        StartCoroutine(createInitialMarbles());
	}

    // Update is called once per frame
    void Update()
    {
		int numberOfMarbles = GameObject.FindGameObjectsWithTag ("Marble").Length;

		if (numberOfMarbles < x && hasRun == true) {
			createMarble ();
		}
    }

    IEnumerator createInitialMarbles ()
    {
        for (int i = 0; i < x; i++)
        {
            yield return new WaitForSeconds(1);
			createMarble ();
        }

		hasRun = true;
    }

	void createMarble()
	{
		int rand = Random.Range(0, 100);

		if (rand > percentOfFake)
		{
			Marble1.GetComponent<marbleBehavior>().isFake = false;
		}
		else
		{
			Marble1.GetComponent<marbleBehavior>().isFake = true;
		}

		rand = Random.Range(1, 4);
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
			offScreenPos = new Vector3(Random.Range(-xBoundary, xBoundary), -yBoundary - 10, 0);
		}
		else
		{
			offScreenPos = new Vector3(-xBoundary - 10, Random.Range(-yBoundary, yBoundary), 0);
		}

		Instantiate (Marble1, offScreenPos, Quaternion.identity);
	}
}
