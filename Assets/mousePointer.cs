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
       

        if (Input.GetKeyDown (KeyCode.Mouse0))
        {
            sr.sprite = lightOn;
        }
        else if (Input.GetKeyUp (KeyCode.Mouse0))
        {
            sr.sprite = lightOff;
        }
	}
}
