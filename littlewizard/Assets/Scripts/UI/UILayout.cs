using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILayout : MonoBehaviour
{
    public GameObject manuPausePrefab;
    private PauseMenu menuInstnace;

   void Update() {

        if (Input.GetKeyDown(KeyCode.Escape)) {

            if (!GameManager.Instance.isGamePaused) {

                GameManager.Instance.pauseGame();
            } else {

                GameManager.Instance.resumeGame();
               
            }
        }
    }

    public void showMenuPause() {
        menuInstnace = Instantiate(manuPausePrefab, transform.Find("/UILayout"), false).GetComponent<PauseMenu>();
    }

    public void onResumeGame() {

        menuInstnace.close();
    }
}
