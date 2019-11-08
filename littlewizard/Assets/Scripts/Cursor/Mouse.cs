using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite shootPoint;

    void Start(){
        Cursor.SetCursor(shootPoint.texture, Vector2.zero, CursorMode.Auto);
     
    }


    
}
