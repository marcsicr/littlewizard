using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SceneEnter : MonoBehaviour
{
    public FadeImg fade;
    public TimelineDirector director;

    public Sprite playerFirstSprite;
    public GameObject textIntroPrefab;
    private SceneText text;


    private void Awake() {

        text = Instantiate(textIntroPrefab, transform.Find("/UILayout"), false).GetComponent<SceneText>();
        AudioListener.pause = true;
        
    }

    void Start(){    
        StartCoroutine(sceneEnterCo());
    }


    private IEnumerator sceneEnterCo() {

        float writeSpeed = 0.03f;
        float timeout = 1f;
        fade.setOpaque();
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(text.writeEffectCo("Once upon a time in a magic land...", writeSpeed,timeout));
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(text.fadeOutCo(timeout));
        yield return StartCoroutine(text.writeEffectCo("There was a young wizard ready to take a mission he would never forget... ", writeSpeed,timeout));
        yield return new WaitForSeconds(1.5f);
        yield return StartCoroutine(text.fadeOutCo(timeout));

        director.play();
        yield return StartCoroutine(fade.onlyFadeIn(0.2f));
       
    }
}
