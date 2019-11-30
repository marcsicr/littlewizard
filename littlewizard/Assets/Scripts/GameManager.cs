using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour{
  public static GameManager Instance { get; private set; }


    public ObservableInt playerHP;


    public Signal gamePause;
    public Signal gameOver;


    private void Awake() {
        
        if(Instance == null) {

            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {

            Destroy(gameObject);
        }
    }


    private void Update() {

        if (Input.GetKeyDown(KeyCode.P)) {

            gamePause.Raise();
        }
    }

}
