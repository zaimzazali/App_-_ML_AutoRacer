using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursorEvents : MonoBehaviour
{
    [SerializeField]
    private Texture2D cursorImg = null;
    
    private void Awake()
    {
        Cursor.SetCursor(cursorImg, Vector2.zero, CursorMode.ForceSoftware);
    }
}
