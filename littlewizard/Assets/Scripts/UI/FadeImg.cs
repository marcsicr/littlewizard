using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeImg : MonoBehaviour
{
    Image img;
    bool fade = false;

    Color whiteAlpha = new Color(0f, 0f, 0f, 0);
    Color whiteFull = new Color(0f, 0f, 0f, 1f);
    //Color whiteAlpha = new Color(1f, 1f, 1f, 0);
    //Color whiteFull = new Color(1f, 1f, 1f, 1f);
    public void Awake() {
        img = gameObject.GetComponent<Image>(); 
    }
    public void FadeOut() {
         StartCoroutine(FadeOutCo(whiteAlpha, whiteFull, 0.2f));
    }

    public void FadeIn() {
        StartCoroutine(FadeInCo(whiteFull,whiteAlpha, 0.2f));
    }

    IEnumerator FadeOutCo(Color start, Color end, float duration) {

        for (float t = 0f; t < duration; t += Time.deltaTime) {
            float normalizedTime = t / duration;
            
            img.color = Color.Lerp(start, end, normalizedTime);
            yield return null;
        }
        img.color = end; 
        fade = true;
    }

    IEnumerator FadeInCo(Color start, Color end, float duration) {

        while (!fade)
            yield return null;

        yield return new WaitForSeconds(0.6f);

        for (float t = 0f; t < duration; t += Time.deltaTime) {
            float normalizedTime = t / duration;
            
            img.color = Color.Lerp(start, end, normalizedTime);
            yield return null;
        }
        img.color = end; 

        fade = false;
    }

    public void setOpaque() {
        img.color = whiteFull;
        
    }

    public IEnumerator onlyFadeIn(float duration) {

        yield return StartCoroutine(OnlyFadeIn(whiteFull, whiteAlpha, duration));
    }
    IEnumerator OnlyFadeIn(Color start, Color end, float duration) {

        yield return new WaitForSeconds(0.6f);

        for (float t = 0f; t < duration; t += Time.deltaTime) {
            float normalizedTime = t / duration;

            img.color = Color.Lerp(start, end, normalizedTime);
            yield return null;
        }
        img.color = end;

        fade = false;
    }
}
