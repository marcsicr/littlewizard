using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimeLineTrigger : MonoBehaviour
{
    public TimelineDirector timeline;


    private void OnTriggerEnter2D(Collider2D other) {

        if (other.CompareTag(Player.TAG)) {

            triggerTimeLine();

   
        }
    }

    public void triggerTimeLine() {

        Debug.Log("Triggering");
        timeline.play();
        Destroy(gameObject,1);
    }
}
