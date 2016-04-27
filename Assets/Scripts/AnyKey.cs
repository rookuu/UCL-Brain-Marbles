using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/* Changes scene is any button is pressed.
 * Used on the splash screen
 */

public class AnyKey : MonoBehaviour {

    void Update()
    {
        if (Input.anyKeyDown)
        {
			SceneManager.LoadScene (1);
        }

    }

}
