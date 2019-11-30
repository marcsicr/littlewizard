using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class TransferListener : MonoBehaviour
{
   [SerializeField]
    public SignalListener enterListener = new SignalListener();
    public SignalListener leaveListene =  new SignalListener();

}
