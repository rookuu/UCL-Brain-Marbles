using UnityEngine;
using System.Collections;

public class volumeToggle : MonoBehaviour
{

    public Sprite VolumeOn, VolumeOff;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        SetVolume(AudioListener.volume != 1 ? false : true);
    }

    void Update()
    {
        if (isClicked())
        {
            SetVolume(AudioListener.volume != 0 ? false : true);
        }
    }

    private void SetVolume(bool IsOn)
    {
        if (IsOn)
        {
            spriteRenderer.sprite = VolumeOn;
            AudioListener.volume = 1;
        }
        else {
            spriteRenderer.sprite = VolumeOff;
            AudioListener.volume = 0;
        }
    }

    public bool isClicked()
    {
        bool result = false;
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos = new Vector2(wp.x, wp.y);
            if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(mousePos))
            {
                result = true;
            }
        }
        return result;
    }



}