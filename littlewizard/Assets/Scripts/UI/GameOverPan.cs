using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class GameOverPan : MonoBehaviour
{
  
    GameObject holder;
    void Awake(){
        holder = transform.Find("Holder").gameObject;

    }

   public void Retry() {

        GameManager.Instance.restartLevel();

        holder.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void exitGame() {

        GameManager.Instance.QuitGame();
    }

    public void onGameOver() {

        StartCoroutine(showCo());
    }

    private IEnumerator showCo() {

        yield return new WaitForSeconds(1);

        
        holder.SetActive(true);
        EventSystem.current.SetSelectedGameObject(transform.Find("Holder/RetryBtn").gameObject);
    }
}
