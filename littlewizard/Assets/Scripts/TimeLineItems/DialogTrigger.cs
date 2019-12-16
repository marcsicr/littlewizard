using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    private void OnEnable() {
        Debug.Log("HasBeenActivated");
        NPC npc = GetComponent<NPC>();
        npc.showDialog();
    }
}
