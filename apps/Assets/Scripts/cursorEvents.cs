using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursorEvents : MonoBehaviour
{
    [SerializeField]
    private Texture2D cursorImg = null;

    [SerializeField]
    private float position_x = 0f, position_y = 0f;
    private Vector2 cursorLocation;
    
    private void Awake()
    {
        cursorLocation = new Vector2(position_x, position_y);
        Cursor.SetCursor(cursorImg, cursorLocation, CursorMode.ForceSoftware);
    }
}
