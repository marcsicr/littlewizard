using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bar : MonoBehaviour
{
    public IntVar watchVar;
   

   
    // Start is called before the first frame update
    void Start()
    {
      
        transform.localScale = new Vector3(1f, 1f);
    }

    // Update is called once per frame
    void Update() {

        if (watchVar.initialValue != watchVar.runtimeValue) {

            float barLength = computeBarLength();
            //Debug.Log("Bar Length:" + barLength.ToString());
            transform.localScale = new Vector3(barLength, 1f);
        }

    }

    /*Return float in range 0 <-> 1*/
    private float computeBarLength() {

        
        if(watchVar.runtimeValue > 0) {

            float maxValue = (float)watchVar.initialValue;
            float currentValue = (float)watchVar.runtimeValue;
            return currentValue /maxValue;
        }

        return 0;
        

    }
}
