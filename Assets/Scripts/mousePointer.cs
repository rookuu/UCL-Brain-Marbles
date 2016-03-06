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

		if (Input.GetMouseButtonDown (0)) {
			RaycastHit2D hitInfo = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);

			if (hitInfo.collider != null) {
				hitInfo.collider.gameObject.GetComponent<marbleBehavior> ().catchMarble ();
			}
		}
	}
}
