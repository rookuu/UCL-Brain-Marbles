﻿using UnityEngine;
using System.Collections;

public class moveRandomly : MonoBehaviour {

    int numberOfPathNodes;
    public int speedOfMarble = 25;
	public int xBoundary;
	public int yBoundary;

	// Use this for initialization
	void Start ()
    {
        numberOfPathNodes = Random.Range(10, 50);
        Vector3[] path = new Vector3[numberOfPathNodes];

        for (int i = 0; i < numberOfPathNodes; i++)
        {
			path[i] = new Vector3(Random.Range(-xBoundary, xBoundary), Random.Range(-yBoundary, yBoundary), 0);
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
}