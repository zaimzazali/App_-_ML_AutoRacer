using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursorEvents : MonoBehaviour
{
    [SerializeField]
    private Texture2D cursorImg = null;

    private float position_x = 11f, position_y = 0f;
    
    private void Awake() {
        Vector2 cursorLocation = new Vector2(position_x, position_y);
        Cursor.SetCursor(cursorImg, cursorLocation, CursorMode.ForceSoftware);
    }
}
