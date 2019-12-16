using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpellState : MonoBehaviour
{
   
    private Image img;
    private Color white = new Color(1, 1, 1, 1);
    private Color transparent = new Color(1, 1, 1, 0);

    public Sprite[] reloadingFrames;
    public Sprite selectedSprite;

    private bool reloading = false;
    Animation anim;
    private void Awake() {

       
        img = GetComponent<Image>();
        

    }

    public void setReloading(Spell spell) {
        //reloading = true;

        reloading = true;
        img.color = white;
        //myAnimator.SetBool("selected", false);
        StartCoroutine(spellTimeOutCo(spell));
        //myAnimator.SetBool("loading",true);


    }

    /*public void finishReloading() {

        myAnimator.SetBool("loading", false);
        reloading = false;
        img.color = transparent;
    }*/


    //public bool setSelected(bool selected) {

    //    bool hasBeenSelected = false;
    //    if (!reloading) {

    //        img.sprite =  myAnimator.SetBool("selected", selected);

    //        if (selected)
    //            img.color = white;
    //        else
    //            img.color = transparent;

    //        hasBeenSelected = true;
    //    }

    //    return hasBeenSelected;


    //}

    public bool setSelected(bool selected) {

        bool hasBeenSelected = false;
        if (!reloading) {

            //myAnimator.SetBool("selected", selected);

            if (selected) {
                img.color = white;
                
                 img.overrideSprite = selectedSprite;
               
            } else {
                img.color = transparent;
                img.overrideSprite = null;
              
            }   

            hasBeenSelected = true;
        }

        return hasBeenSelected;

        
    }

    private IEnumerator spellTimeOutCo(Spell spell) {


        float timeOut = LevelManager.Instance.computeSpellTimeOut(spell);
        float frameRate = timeOut / reloadingFrames.Length;

       

        foreach (Sprite frame in reloadingFrames) {
            img.overrideSprite = frame;
            yield return new WaitForSeconds(frameRate);
        }

        img.color = transparent;
        reloading = false;
    }

}
