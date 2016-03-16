using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class AnyKey : MonoBehaviour {

    void Update()
    {
        if (Input.anyKeyDown)
        {
			SceneManager.LoadScene (1);
        }

    }

}
