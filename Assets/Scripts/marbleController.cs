using UnityEngine;
using System.Collections;

public class marbleController : MonoBehaviour {
	public int x, percentOfFake, maxSpeed, xBoundary, yBoundary;
    private bool hasRun = false;

	public GameObject Marble1;
	public Sprite real1, fake1;

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
			Marble1.GetComponent<SpriteRenderer> ().sprite = real1;
		}
		else
		{
			Marble1.GetComponent<marbleBehavior>().isFake = true;
			Marble1.GetComponent<SpriteRenderer> ().sprite = fake1;
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
		Marble1.GetComponent<moveRandomly>().speedOfMarble = maxSpeed;


		Instantiate (Marble1, offScreenPos, Quaternion.identity);
	}
}
