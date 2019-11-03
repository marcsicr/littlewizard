﻿using System.Collections;
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

        SceneManager.LoadScene(sceneIndex);
        holder.SetActive(false);
    }

    public void exitGame() {
        Application.Quit();
    }

    public void show() {

        holder.SetActive(true);
    }
}
