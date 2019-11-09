using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite shootPointer;
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

    public void setShotCursor() {
       Cursor.SetCursor(shootPointer.texture, new Vector2((float)shootPointer.texture.width / 2, (float)shootPointer.texture.height / 2), CursorMode.Auto);
    }
}
