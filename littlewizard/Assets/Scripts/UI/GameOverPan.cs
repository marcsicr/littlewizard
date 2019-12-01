using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPan : MonoBehaviour
{
    int sceneIndex;
    GameObject holder;
    void Awake()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        holder = transform.Find("Holder").gameObject;
    }

   public void Retry() {

        GameManager.Instance.restartLevel();
        holder.SetActive(false);
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
    }
}
