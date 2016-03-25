using UnityEngine;
using System.Collections;

public class marbleController : MonoBehaviour {
	public int marblesOnScreen, xBoundary, yBoundary;
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
	public int minNodes1;
	public int maxNodes1;
	public float size1;
	public int rotation1;
	public int relativeChance1;
	public int scoreChange1;

	[Space(5)]
	[Header("Marble 2")]
	public GameObject Marble2;
	public Sprite sprite2;
	public bool fake2;
	public int speed2;
	public string movementScript2;
	public int minNodes2;
	public int maxNodes2;
	public float size2;
	public int rotation2;
	public int relativeChance2;
	public int scoreChange2;

	[Space(5)]
	[Header("Marble 3")]
	public GameObject Marble3;
	public Sprite sprite3;
	public bool fake3;
	public int speed3;
	public string movementScript3;
	public int minNodes3;
	public int maxNodes3;
	public float size3;
	public int rotation3;
	public int relativeChance3;
	public int scoreChange3;

	[Space(5)]
	[Header("Marble 4")]
	public GameObject Marble4;
	public Sprite sprite4;
	public bool fake4;
	public int speed4;
	public string movementScript4;
	public int minNodes4;
	public int maxNodes4;
	public float size4;
	public int rotation4;
	public int relativeChance4;
	public int scoreChange4;

	[Space(5)]
	[Header("Marble 5")]
	public GameObject Marble5;
	public Sprite sprite5;
	public bool fake5;
	public int speed5;
	public string movementScript5;
	public int minNodes5;
	public int maxNodes5;
	public float size5;
	public int rotation5;
	public int relativeChance5;
	public int scoreChange5;

	[Space(5)]
	[Header("Marble 6")]
	public GameObject Marble6;
	public Sprite sprite6;
	public bool fake6;
	public int speed6;
	public string movementScript6;
	public int minNodes6;
	public int maxNodes6;
	public float size6;
	public int rotation6;
	public int relativeChance6;
	public int scoreChange6;

	// Use this for initialization
	void Start () {
		StartCoroutine(createInitialMarbles());
	}

	// Update is called once per frame
	void Update()
	{
		if (isRunning == true) {
			int numberOfMarbles = GameObject.FindGameObjectsWithTag ("Marble").Length;

			if (numberOfMarbles < marblesOnScreen && hasRun == true) {
				createMarble ();
			}
		}
	}

	IEnumerator createInitialMarbles ()
	{
		for (int i = 0; i < marblesOnScreen; i++)
		{
			yield return new WaitForSeconds(1);
			createMarble ();
		}

		hasRun = true;
	}

	void createMarble()
	{
		int rand = Random.Range (0, calcTotalChance (uniqueMarbles)); 
		GameObject MarbleX;

		if (rand < relativeChance1) {
			MarbleX = Marble1;
			MarbleX = enableMovementScript (MarbleX, movementScript1, maxNodes1, minNodes1);
			MarbleX.GetComponent<marbleBehavior> ().isFake = fake1;
			MarbleX.GetComponent<SpriteRenderer> ().sprite = sprite1;
			MarbleX.GetComponent<marbleBehavior> ().updateSpeed (speed1);
			MarbleX.transform.localScale = new Vector3 (size1, size1, size1);
			MarbleX.GetComponent<marbleBehavior> ().scoreChange = scoreChange1;
		} else if (rand < (relativeChance1 + relativeChance2)) {
			MarbleX = Marble2;
			MarbleX = enableMovementScript (MarbleX, movementScript2, maxNodes2, minNodes2);
			MarbleX.GetComponent<marbleBehavior> ().isFake = fake2;
			MarbleX.GetComponent<SpriteRenderer> ().sprite = sprite2;
			MarbleX.GetComponent<marbleBehavior> ().updateSpeed (speed2);
			MarbleX.transform.localScale = new Vector3 (size2, size2, size2);
			MarbleX.GetComponent<marbleBehavior> ().scoreChange = scoreChange2;
		} else if (rand < (relativeChance1 + relativeChance2 + relativeChance3)) {
			MarbleX = Marble3;
			MarbleX = enableMovementScript (MarbleX, movementScript3, maxNodes3, minNodes3);
			MarbleX.GetComponent<marbleBehavior> ().isFake = fake3;
			MarbleX.GetComponent<SpriteRenderer> ().sprite = sprite3;
			MarbleX.GetComponent<marbleBehavior> ().updateSpeed (speed3);
			MarbleX.transform.localScale = new Vector3 (size3, size3, size3);
			MarbleX.GetComponent<marbleBehavior> ().scoreChange = scoreChange3;
		} else if (rand < (relativeChance1 + relativeChance2 + relativeChance3 + relativeChance4)) {
			MarbleX = Marble4;
			MarbleX = enableMovementScript (MarbleX, movementScript4, maxNodes4, minNodes4);
			MarbleX.GetComponent<marbleBehavior> ().isFake = fake4;
			MarbleX.GetComponent<SpriteRenderer> ().sprite = sprite4;
			MarbleX.GetComponent<marbleBehavior> ().updateSpeed (speed4);
			MarbleX.transform.localScale = new Vector3 (size4, size4, size4);
			MarbleX.GetComponent<marbleBehavior> ().scoreChange = scoreChange4;
		} else if (rand < (relativeChance1 + relativeChance2 + relativeChance3 + relativeChance4 + relativeChance5)) {
			MarbleX = Marble5;
			MarbleX = enableMovementScript (MarbleX, movementScript5, maxNodes5, minNodes5);
			MarbleX.GetComponent<marbleBehavior> ().isFake = fake5;
			MarbleX.GetComponent<SpriteRenderer> ().sprite = sprite5;
			MarbleX.GetComponent<marbleBehavior> ().updateSpeed (speed5);
			MarbleX.transform.localScale = new Vector3 (size5, size5, size5);
			MarbleX.GetComponent<marbleBehavior> ().scoreChange = scoreChange5;
		} else {
			MarbleX = Marble6;
			MarbleX = enableMovementScript (MarbleX, movementScript6, maxNodes6, minNodes6);
			MarbleX.GetComponent<marbleBehavior> ().isFake = fake6;
			MarbleX.GetComponent<SpriteRenderer> ().sprite = sprite6;
			MarbleX.GetComponent<marbleBehavior> ().updateSpeed (speed6);
			MarbleX.transform.localScale = new Vector3 (size6, size6, size6);
			MarbleX.GetComponent<marbleBehavior> ().scoreChange = scoreChange6;
		}

		int rand2 = Random.Range(1, 4);
		Vector3 offScreenPos;

		if (rand2 == 1)
		{
			offScreenPos = new Vector3(Random.Range(-xBoundary, xBoundary), yBoundary + 10, 0);
		}
		else if (rand2 == 2)
		{
			offScreenPos = new Vector3(xBoundary + 10, Random.Range(-yBoundary, yBoundary), 0);
		}
		else if (rand2 == 3)
		{
			offScreenPos = new Vector3(Random.Range(-xBoundary, xBoundary), -yBoundary - 10, 0);
		}
		else
		{
			offScreenPos = new Vector3(-xBoundary - 10, Random.Range(-yBoundary, yBoundary), 0);
		}

		GameObject newMarble = (GameObject)Instantiate (MarbleX, offScreenPos, Quaternion.identity);

		if (rand < relativeChance1) {
			newMarble.transform.Rotate (new Vector3 (0, 0, rotation1));
		} 
		else if (rand < (relativeChance1 + relativeChance2)) {
			newMarble.transform.Rotate (new Vector3 (0, 0, rotation2));
		} 
		else if (rand < (relativeChance1 + relativeChance2 + relativeChance3)) 
		{
			newMarble.transform.Rotate (new Vector3 (0, 0, rotation3));
		} 
		else if (rand < (relativeChance1 + relativeChance2 + relativeChance3 + relativeChance4)) 
		{
			newMarble.transform.Rotate (new Vector3 (0, 0, rotation4));
		} 
		else if (rand < (relativeChance1 + relativeChance2 + relativeChance3 + relativeChance4 + relativeChance5)) 
		{
			newMarble.transform.Rotate (new Vector3 (0, 0, rotation5));
		} 
		else 
		{
			newMarble.transform.Rotate (new Vector3 (0, 0, rotation6));
		}
	}

	private GameObject enableMovementScript(GameObject marbleX, string name, int minNodes, int maxNodes) 
	// For New Movement Scripts (Identities) They'll need to be enabled / disabled here.
	{
		if (name == "moveRandomly") {
			marbleX.GetComponent<moveRandomly> ().enabled = true;
			marbleX.GetComponent<moveRandomly> ().maxNodes = maxNodes;
			marbleX.GetComponent<moveRandomly> ().minNodes = minNodes;
		}

		return marbleX;
	}

	private int calcTotalChance(int uniqueMarbles)
	{
		int total = 0;

		if (uniqueMarbles >= 1) {
			total += relativeChance1;
		} 
		if (uniqueMarbles >= 2) {
			total += relativeChance2;
		} 
		if (uniqueMarbles >= 3) {
			total += relativeChance3;
		}
		if (uniqueMarbles >= 4) {
			total += relativeChance4;
		}
		if (uniqueMarbles >= 5) {
			total += relativeChance5;
		}
		if (uniqueMarbles >= 6) {
			total += relativeChance6;
		}

		return total;
	}
}
