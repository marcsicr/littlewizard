using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageTrigger : MonoBehaviour
{
    private void OnEnable() {

        DialogManager.Instance.displayMessage("Farlopa");
    }
}
