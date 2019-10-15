using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bar : MonoBehaviour
{
    public FloatVar watchVar;
   

   
    // Start is called before the first frame update
    void Start()
    {
      
        transform.localScale = new Vector3(1f, 1f);
    }

    // Update is called once per frame
    void Update() {

        if (watchVar.initialValue != watchVar.runtimeValue) {

          //  Debug.Log("Player SP:" + watchVar.runtimeValue.ToString());
            float barLength = computeBarLength();
            transform.localScale = new Vector3(barLength, 1f);

        }

    }

    /*Return float in range 0 <-> 1*/
    private float computeBarLength() {
       
        
        if(watchVar.runtimeValue > 0) {
            return watchVar.runtimeValue / watchVar.initialValue;
        }

        return 0;
        

    }
}
