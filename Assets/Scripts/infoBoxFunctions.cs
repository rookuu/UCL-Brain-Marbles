using UnityEngine;
using System.Collections;

public class infoBoxFunctions : MonoBehaviour {

	public void closeBox() {
		gameObject.SetActive (false);
	}

	public void startGame() {
		GameObject.Find ("marbleController").GetComponent<marbleController> ().startGame ();
		gameObject.SetActive (false);
	}
}
