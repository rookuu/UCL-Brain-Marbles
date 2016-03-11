using UnityEngine;
using System.Collections;

public class marbleController : MonoBehaviour {
	public int x, percentOfFake, maxSpeed, xBoundary, yBoundary;
    private bool hasRun = false;
	public int uniqueMarbles;

	public GameObject Marble1;
	public Sprite sprite1;
	public bool fake1;

	public GameObject Marble2;
	public Sprite sprite2;
	public bool fake2;

	public GameObject Marble3;
	public Sprite sprite3;
	public bool fake3;

	public GameObject Marble4;
	public Sprite sprite4;
	public bool fake4;

	public GameObject Marble5;
	public Sprite sprite5;
	public bool fake5;

	public GameObject Marble6;
	public Sprite sprite6;
	public bool fake6;

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
		int rand = Random.Range (0, uniqueMarbles); //Want % for each Marble?
		GameObject MarbleX;

		if (rand == 0) {
			MarbleX = Marble1;
			MarbleX.GetComponent<marbleBehavior> ().isFake = fake1;
			MarbleX.GetComponent<SpriteRenderer> ().sprite = sprite1;
		} else if (rand == 1) {
			MarbleX = Marble2;
			MarbleX.GetComponent<marbleBehavior> ().isFake = fake2;
			MarbleX.GetComponent<SpriteRenderer> ().sprite = sprite2;
		} else if (rand == 2) {
			MarbleX = Marble3;
			MarbleX.GetComponent<marbleBehavior> ().isFake = fake3;
			MarbleX.GetComponent<SpriteRenderer> ().sprite = sprite3;
		} else if (rand == 3) {
			MarbleX = Marble4;
			MarbleX.GetComponent<marbleBehavior> ().isFake = fake4;
			MarbleX.GetComponent<SpriteRenderer> ().sprite = sprite4;
		} else if (rand == 4) {
			MarbleX = Marble5;
			MarbleX.GetComponent<marbleBehavior> ().isFake = fake5;
			MarbleX.GetComponent<SpriteRenderer> ().sprite = sprite5;
		} else {
			MarbleX = Marble6;
			MarbleX.GetComponent<marbleBehavior> ().isFake = fake6;
			MarbleX.GetComponent<SpriteRenderer> ().sprite = sprite6;
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

		MarbleX.GetComponent<moveRandomly>().speedOfMarble = maxSpeed;

		Instantiate (MarbleX, offScreenPos, Quaternion.identity);
	}
}
