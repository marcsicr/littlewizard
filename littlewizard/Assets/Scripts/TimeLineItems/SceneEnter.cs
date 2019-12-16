using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SceneEnter : MonoBehaviour
{
    public FadeImg fade;
    public TimelineDirector director;


    void Start()
    {
        float timeout = 2f;
        fade.setOpaque();
        fade.onlyFadeIn(timeout);
        StartCoroutine(waitFadeCo(timeout));
    }


    IEnumerator waitFadeCo(float timeout) {

        yield return null;
        director.play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
