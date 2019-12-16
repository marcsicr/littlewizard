using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogMessage{

    public bool isPlayer;
    public string charName;
    [TextArea(3, 10)]
    public string message;
    public Sprite charImage;
}
