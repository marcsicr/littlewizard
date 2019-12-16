using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyclopesShooting : StateMachineBehaviour {

    
    Cyclopes cyclope;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        cyclope = animator.GetComponent<Cyclopes>();
        cyclope.outLineEffectEnabled(true);
        cyclope.StartCoroutine(laserShotCo(animator));
        
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        
                     
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
       
    }

     IEnumerator laserShotCo(Animator myAnimator) {

        cyclope.isShooting = true;
        cyclope.startStun();
        float timeOut = cyclope.laserDuration;
        LaserBeam laser = cyclope.laser;
        yield return null;
        laser.transform.position = cyclope.eyePoint.position;
        laser.shot(cyclope.getDirectionToPlayer());

        
        while (timeOut > 0) {

            float percent = timeOut / cyclope.laserDuration;
            float currentSP = Mathf.Clamp(cyclope.BossSP.getRunTimeValue() *percent, 0, cyclope.BossSP.initialValue);
            cyclope.BossSP.UpdateValue(currentSP);
            timeOut -= Time.deltaTime;
            yield return null;
        }
        cyclope.outLineEffectEnabled(false);
        yield return cyclope.StartCoroutine(laser.dissapear());
        myAnimator.SetBool("shooting", false);
        cyclope.isLaserStateSheduled = false;
        cyclope.isShooting = false;
    }

}
