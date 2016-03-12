using UnityEngine;
using System.Collections;

public class marbleBehavior : MonoBehaviour {

    public bool isFake = false;
	GameObject levelController;

    void Start ()
    {
		levelController = GameObject.FindGameObjectWithTag ("GameController");
    }

    public void catchMarble ()
    {
            if (isFake == false)
            {
                Destroy(gameObject);
			levelController.GetComponent<levelController>().addScore (200);

	
            }
            else
            {
			Destroy(gameObject);
			levelController.GetComponent<levelController>().removeScore (100);
            }


	}

	public void badCatch ()
	{
		Destroy(gameObject);
		levelController.GetComponent<levelController> ().removeScore (100);
	}

	public void updateSpeed(int speed) {
		if (GetComponent<moveRandomly> ().enabled == true) {
			GetComponent<moveRandomly> ().speedOfMarble = speed;
		}
	}
}
