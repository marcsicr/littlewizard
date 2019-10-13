using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FloatVar : ScriptableObject,ISerializationCallbackReceiver
{
    public float initialValue;


    [System.NonSerialized]
    public float runtimeValue;

    public void OnAfterDeserialize() {
        runtimeValue = initialValue;
    }

    public void OnBeforeSerialize() {
        
    }

 

}
