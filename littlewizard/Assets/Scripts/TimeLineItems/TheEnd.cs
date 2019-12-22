﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheEnd : MonoBehaviour
{
    public FadeImg fade;
    public GameObject sceneTextPrefab;

    SceneText text;

    public void OnEnable() {

        Debug.Log("Enabled");
        StartCoroutine(theEndCo());
        text = Instantiate(sceneTextPrefab, transform.Find("/UILayout"), false).GetComponent<SceneText>();
    }

    public IEnumerator theEndCo() {


        yield return StartCoroutine(fade.onlyFadeOut(2));
        yield return StartCoroutine(text.writeEffectCo("THE END", 0.2f, 4,TMPro.TextAlignmentOptions.Center,72));
        yield return StartCoroutine(text.fadeOutCo(4));
        

        GameManager.Instance.goToMainMenu();
    }
}