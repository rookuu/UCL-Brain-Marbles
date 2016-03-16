using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {
	
	public void ChangeToScene (int sceneChangeTo) {

		SceneManager.LoadScene (sceneChangeTo);
	
	}
}
