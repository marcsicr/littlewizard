using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorControllerFix : MonoBehaviour
{
    private Animator animator;
    private RuntimeAnimatorController controller;

    private void Awake() {
        animator = GetComponent<Animator>();
        controller = animator.runtimeAnimatorController;
        animator.runtimeAnimatorController = null;
    }


    public void setAnimator() {
        animator.runtimeAnimatorController = controller;
    }
    
}
