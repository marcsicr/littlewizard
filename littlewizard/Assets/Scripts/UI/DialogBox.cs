using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogBox : MonoBehaviour
{
    Image img;
    GameObject spaceImg;
    TextMeshProUGUI charName;
    TextMeshProUGUI message;
    Queue<DialogMessage> dialog;

    public Signal dialogStart;
    public Signal dialogEnd;

    private void Awake() {
        img = transform.Find("Image").GetComponent<Image>();
        charName = transform.Find("Name").GetComponent<TextMeshProUGUI>();
        message = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        spaceImg = transform.Find("Space").gameObject;
    }

    

    public void displayDialog(DialogMessage[] dialogMessages) {

       
        dialog = new Queue<DialogMessage>();
        
        foreach (DialogMessage message in dialogMessages) {
            dialog.Enqueue(message);
        }

        StartCoroutine(displayConversationCo());

    }

    private IEnumerator displayConversationCo() {
        
        dialogStart.Raise();
        while (dialog.Count > 0) {
            message.text = "";
            spaceImg.SetActive(false);
            DialogMessage dialogMessage = dialog.Dequeue();
            charName.text = dialogMessage.charName;
            img.sprite = dialogMessage.charImage;

            foreach (char c in dialogMessage.message.ToCharArray()) {

                message.text += c;
                yield return null;
            }
            spaceImg.SetActive(true);

            while (!Input.GetKeyDown(KeyCode.Space)) {

                yield return null;
            }
        }

        dialogEnd.Raise();
        Destroy(gameObject);

    }
}
