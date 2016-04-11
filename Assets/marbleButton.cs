using UnityEngine;
using System.Collections;

public class marbleButton : MonoBehaviour {
	void Start() {

	}

	public void viewData() {
		Debug.Log(gameObject.transform.GetSiblingIndex ());
	}
}
