using UnityEngine;
using System.Collections;

public class mousePointer : MonoBehaviour {

    public Sprite lightOn;
    public Sprite lightOff;

    private SpriteRenderer sr;

    // Use this for initialization
    void Start () {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = lightOff;
	}
	
	// Update is called once per frame
	void Update () {
        var mousePos = Input.mousePosition;
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10));

	}

	void OnTriggerStay2D (Collider2D coll)
	{
		if (Input.GetKeyDown(KeyCode.Mouse0) && coll.tag == "Marble") {
			coll.gameObject.GetComponent<marbleBehavior> ().catchMarble ();
		}

	}

}
