using UnityEngine;
using System.Collections;

public class AnyKey : MonoBehaviour {

    void Update()
    {
        if (Input.anyKeyDown)
        {
            Application.LoadLevel(1);
        }

    }

}
