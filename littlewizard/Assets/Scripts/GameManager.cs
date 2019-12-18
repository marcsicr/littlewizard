using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour{
  public static GameManager Instance { get; private set; }

    public bool isGamePaused { get; private set; }

    private AsyncOperation asyncLoad;

    public ObservableInteger playerHP;

    public int baseBoltLvl { get; private set; }
    public int baseShieldLvl { get; private set; }
    public int baseRayLvl { get; private set; }


    IntegerObserver valueObserver;

    public Signal gamePauseSignal;
    public Signal gameResumeSignal;

    public Signal levelCompleteSignal;
    public Signal gameOverSignal;

 
    private void onPlayerHPChanged(int newValue) {

        //Debug.Log(newValue);

        if (newValue <= 0) {
            gameOverSignal.Raise();
        }
    }



    private void Awake() {
        
        if(Instance == null) {

            Instance = this;
            GameManager.Instance.valueObserver = new IntegerObserver(playerHP, onPlayerHPChanged);

            DontDestroyOnLoad(gameObject);
        }else {

            Destroy(gameObject);
        }
    }

  
    private void Update() {

        
        if (Input.GetKeyDown(KeyCode.P)) {

            if (!isGamePaused) {
                pauseGame();
            } else {
                resumeGame();
            }
            
        }
    }

    public void pauseGame() {

        isGamePaused = true;
        Time.timeScale = 0;
        gamePauseSignal.Raise();

    }

    public void resumeGame() {

        isGamePaused = false;
        Time.timeScale = 1;
        gameResumeSignal.Raise();
        

    }

    public void QuitGame() {

        Application.Quit();
    }

    

    private void OnApplicationQuit() {

        GameManager.Instance.valueObserver.stopObserving(); //Important!
        
    }


    public void restartLevel() {


        Destroy(LevelManager.Instance.gameObject);
        Destroy(DialogManager.Instance.gameObject);
        // LevelManager.Instance.resetInstance();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void goToNextLevel() {

        Destroy(LevelManager.Instance.gameObject);
        Destroy(DialogManager.Instance.gameObject);
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        levelCompleteSignal.Raise();

        SceneManager.LoadScene(nextSceneIndex);
    }

    public void goToFirstLevel() {

        asyncLoad.allowSceneActivation = true;
        Destroy(LevelManager.Instance.gameObject);
    }


    public void loadFirstLevelAsync() {

        asyncLoad = SceneManager.LoadSceneAsync(2);
        asyncLoad.allowSceneActivation = false;
    }
    
}
