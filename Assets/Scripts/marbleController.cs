using UnityEngine;
using System.Collections;

public class marbleController : MonoBehaviour {
	public int x, percentOfFake, xBoundary, yBoundary;
    private bool hasRun = false;
	public bool isRunning = true;
	public int uniqueMarbles;

	[Space(10)]
	[Header("Marble 1")]
	public GameObject Marble1;
	public Sprite sprite1;
	public bool fake1;
	public int speed1;
	public string movementScript1;

	[Space(5)]
	[Header("Marble 2")]
	public GameObject Marble2;
	public Sprite sprite2;
	public bool fake2;
	public int speed2;
	public string movementScript2;

	[Space(5)]
	[Header("Marble 3")]
	public GameObject Marble3;
	public Sprite sprite3;
	public bool fake3;
	public int speed3;
	public string movementScript3;

	[Space(5)]
	[Header("Marble 4")]
	public GameObject Marble4;
	public Sprite sprite4;
	public bool fake4;
	public int speed4;
	public string movementScript4;

	[Space(5)]
	[Header("Marble 5")]
	public GameObject Marble5;
	public Sprite sprite5;
	public bool fake5;
	public int speed5;
	public string movementScript5;

	[Space(5)]
	[Header("Marble 6")]
	public GameObject Marble6;
	public Sprite sprite6;
	public bool fake6;
	public int speed6;
	public string movementScript6;

	// Use this for initialization
	void Start () {
        StartCoroutine(createInitialMarbles());
	}

    // Update is called once per frame
    void Update()
    {
		if (isRunning == true) {
			int numberOfMarbles = GameObject.FindGameObjectsWithTag ("Marble").Length;

			if (numberOfMarbles < x && hasRun == true) {
				createMarble ();
			}
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
			MarbleX = enableMovementScript (MarbleX, movementScript1);
			MarbleX.GetComponent<marbleBehavior> ().isFake = fake1;
			MarbleX.GetComponent<SpriteRenderer> ().sprite = sprite1;
			MarbleX.GetComponent<marbleBehavior> ().updateSpeed (speed1);
		} else if (rand == 1) {
			MarbleX = Marble2;
			MarbleX = enableMovementScript (MarbleX, movementScript2);
			MarbleX.GetComponent<marbleBehavior> ().isFake = fake2;
			MarbleX.GetComponent<SpriteRenderer> ().sprite = sprite2;
			MarbleX.GetComponent<marbleBehavior> ().updateSpeed (speed2);
		} else if (rand == 2) {
			MarbleX = Marble3;
			MarbleX = enableMovementScript (MarbleX, movementScript3);
			MarbleX.GetComponent<marbleBehavior> ().isFake = fake3;
			MarbleX.GetComponent<SpriteRenderer> ().sprite = sprite3;
			MarbleX.GetComponent<marbleBehavior> ().updateSpeed (speed3);
		} else if (rand == 3) {
			MarbleX = Marble4;
			MarbleX = enableMovementScript (MarbleX, movementScript4);
			MarbleX.GetComponent<marbleBehavior> ().isFake = fake4;
			MarbleX.GetComponent<SpriteRenderer> ().sprite = sprite4;
			MarbleX.GetComponent<marbleBehavior> ().updateSpeed (speed4);
		} else if (rand == 4) {
			MarbleX = Marble5;
			MarbleX = enableMovementScript (MarbleX, movementScript5);
			MarbleX.GetComponent<marbleBehavior> ().isFake = fake5;
			MarbleX.GetComponent<SpriteRenderer> ().sprite = sprite5;
			MarbleX.GetComponent<marbleBehavior> ().updateSpeed (speed5);
		} else {
			MarbleX = Marble6;
			MarbleX = enableMovementScript (MarbleX, movementScript6);
			MarbleX.GetComponent<marbleBehavior> ().isFake = fake6;
			MarbleX.GetComponent<SpriteRenderer> ().sprite = sprite6;
			MarbleX.GetComponent<marbleBehavior> ().updateSpeed (speed6);
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
			
		Instantiate (MarbleX, offScreenPos, Quaternion.identity);
	}

	private GameObject enableMovementScript(GameObject marbleX, string name) 
	// For New Movement Scripts (Identities) They'll need to be enabled / disabled here.
	{
		if (name == "moveRandomly") {
			marbleX.GetComponent<moveRandomly> ().enabled = true;
		}

		return marbleX;
	}
}
