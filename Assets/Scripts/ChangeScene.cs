﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {
	
	public void ChangeToScene (int sceneChangeTo) {

		SceneManager.LoadScene (sceneChangeTo);
	
	}

	public void NextLevel () {
		string txt = GameObject.Find ("GlobalData").GetComponent<globalData> ().btnText;
		GameObject.Find ("GlobalData").GetComponent<globalData> ().btnText = (int.Parse (txt) + 1).ToString();
		SceneManager.LoadScene (8);
	}
}
