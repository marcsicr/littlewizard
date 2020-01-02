using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
  
    public Sprite defaultCursor;
    void Start(){

        setDefaultCursor();
    }

    private void Update() {

        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursorPos;
    }

    public void setDefaultCursor() {
       Cursor.SetCursor(defaultCursor.texture, new Vector2(0,0), CursorMode.Auto);
    }
}
