using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class levelBtnClick : MonoBehaviour {

	void Awake() {
		gameObject.GetComponent<Button> ().onClick.AddListener (moveScene);
	}

	void moveScene() {
		globalData data = GameObject.Find ("GlobalData").GetComponent<globalData> ();
		data.btnText = gameObject.name;
		data.saveData ();

		SceneManager.LoadScene (7);
	}
}
