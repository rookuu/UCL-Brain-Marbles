using UnityEngine;
using System.Collections;

public class marbleBehavior : MonoBehaviour {

    public bool isFake = false;
    public Sprite real;
    public Sprite fake;

    void Start ()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        if (isFake == false)
        {
            sr.sprite = real;
        }
        else
        {
            sr.sprite = fake;
        }
    }

    public void catchMarble ()
    {
            if (isFake == false)
            {
                Debug.Log("Real Marble was Caught!");
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Fake Marble was Caught!");
                Destroy(gameObject);
            }


    }
}
