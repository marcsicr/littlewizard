using System.Collections; 
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SceneText : MonoBehaviour {
    TextMeshProUGUI text;

    private void Awake() {

        text = GetComponent<TextMeshProUGUI>();
    }

    private void Start() {
        text.text = "";
    }


    public IEnumerator fadeOutCo(float duration) {

        Color end = new Color32(1, 1, 1, 0);

        for (float t = 0f; t < duration; t += Time.deltaTime) {
            float normalizedTime = t / duration;

            text.color = Color.Lerp(text.color, end, normalizedTime);
            yield return null;
        }
        text.color = end;
    }




    public IEnumerator writeEffectCo(string message, float charTimeout, float fadeTimeout, TextAlignmentOptions ops = TextAlignmentOptions.Left, int fontSize = 42) {

        text.alignment = ops;
        text.fontSize = fontSize;
        text.color = Color.white;
        text.text = "";
        yield return null;

        foreach (char c in message.ToCharArray()) {

            text.text += c;
            yield return new WaitForSeconds(charTimeout);


        }
    }

    public IEnumerator appendWriteEffectCo(string message, float charTimeout) {

        text.text += "\n";
        foreach (char c in message.ToCharArray()) {

            text.text += c;
            yield return new WaitForSeconds(charTimeout);
        }
    }

    
}
