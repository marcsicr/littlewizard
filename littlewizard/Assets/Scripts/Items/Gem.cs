using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public IntVar gemsOnLevel;
    public IntVar gemsCaught;
   
    public void Awake() {

        gemsOnLevel.runtimeValue += 1;
        
    }
    public void OnTriggerEnter2D(Collider2D other) {
        
        if(other.tag == "Player") {
            Debug.Log("Player collided with gem");

            Destroy(this.gameObject);
            gemsCaught.runtimeValue++;
        }
    }

}
