using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpellState : MonoBehaviour
{
    private Animator myAnimator;
    private Image img;
    private Color white = new Color(1, 1, 1, 1);
    private Color transparent = new Color(1, 1, 1, 0);

    private bool reloading = false;
    Animation anim;
    private void Awake() {

        myAnimator = GetComponent<Animator>();
        img = GetComponent<Image>();
        

    }

    

    public void setReloadSpeed(float speed) {
        myAnimator.speed = speed;
    }

    public void setReloading() {
        reloading = true;
        img.color = white;
        
        myAnimator.SetBool("loading",true);
        myAnimator.SetBool("selected", false);

    }

    public void finishReloading() {

        myAnimator.SetBool("loading", false);
        reloading = false;
        img.color = transparent;
    }

    public bool setSelected(bool selected) {

        bool hasBeenSelected = false;
        if (!reloading) {

            myAnimator.SetBool("selected", selected);

            if (selected)
                img.color = white;
            else
                img.color = transparent;

            hasBeenSelected = true;
        }

        return hasBeenSelected;

        
    }

    public void setCasted() {
        reloading = true;
    }

}
