using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class IntVar : ScriptableObject,ISerializationCallbackReceiver
{
    public int initialValue;


    [System.NonSerialized]
    public int runtimeValue;

    public void OnAfterDeserialize() {
        runtimeValue = initialValue;
    }

    public void OnBeforeSerialize() {
        
    }

    public void reset() {
        runtimeValue = initialValue;
    }

 

}
