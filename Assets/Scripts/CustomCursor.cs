using UnityEngine;
using System.Collections;

/* Sets the cursor to the flashlight, */

public class CustomCursor : MonoBehaviour
{
    public Texture2D cursorTexture;

    void Start()
    {
		Cursor.SetCursor(this.cursorTexture, Vector2.zero, CursorMode.Auto);
    }
}