﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class DialogManager : MonoBehaviour {

    public static DialogManager Instance { get; private set; }
    public GameObject dialogPrefab;
    public GameObject singMessagePrefab;

    public Signal DialogStart;
    public Signal DialogEnd;
   
    SignMessageBox messageBox;

    private void Awake() {

        if (Instance == null) {
            Instance = this;

            //SceneManager.sceneLoaded += OnLevelFinishedLoading;
            DontDestroyOnLoad(gameObject);
        } else {

            Destroy(gameObject);
        }
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode) {
        //dialogBox = transform.Find("/UILayout/DialogBox").gameObject;
        //messageBox = transform.Find("/UILayout/SignMessageBox").gameObject.GetComponent<SignMessageBox>();
        //messageBox.gameObject.SetActive(false);
        //dialog = new Queue<DialogMessage>();
        //dialogBox.SetActive(false); 
    }
    

    public void displayMessage(string message) {

        Transform hud = transform.Find("/UILayout");
        GameObject box = Instantiate(singMessagePrefab, hud, false);
        
        messageBox = box.GetComponent<SignMessageBox>();
        messageBox.show(message);
    }

    public void hideMessage() {
        if(messageBox != null) {
            messageBox.hide();
            messageBox = null;
        }
    }

    public void displayConversation(DialogMessage[] messages) {

        Transform hud = transform.Find("/UILayout");
        GameObject currentBox = Instantiate(dialogPrefab, hud, false);
        currentBox.GetComponent<DialogBox>().displayDialog(messages);

    }
}