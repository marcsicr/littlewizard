using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogManager : MonoBehaviour {

    public static DialogManager Instance { get; private set; }
    public Signal DialogStart;
    public Signal DialogEnd;
    GameObject dialogBox;
    SignMessageBox messageBox;

    bool displaying = false;

    private Queue<DialogMessage> dialog;

    private void Awake() {

        if (Instance == null) {
            Instance = this;

            dialogBox = transform.Find("/UILayout/DialogBox").gameObject;
            messageBox = transform.Find("/UILayout/SignMessageBox").gameObject.GetComponent<SignMessageBox>();
            messageBox.gameObject.SetActive(false);
            dialog = new Queue<DialogMessage>();
            dialogBox.SetActive(false);
            DontDestroyOnLoad(gameObject);
        } else {

            Destroy(gameObject);
        }
    }

    public void displayMessage(string message) {
        messageBox.show(message);
    }

    public void displayConversation(DialogMessage[] messages) {

        if (!displaying) {

            DialogStart.Raise();
            dialogBox.gameObject.SetActive(true);
            displaying = true;
            dialog.Clear();
            foreach (DialogMessage message in messages) {
                dialog.Enqueue(message);
            }

            StartCoroutine(displayMessageCo());
        }

    }

    public void hideConversation() {

        StopAllCoroutines();
        gameObject.SetActive(false);
    }

    IEnumerator displayMessageCo() {

        Debug.Log("Displaying conversation");
        Image img = dialogBox.transform.Find("Image").GetComponent<Image>();
        TextMeshProUGUI charName = dialogBox.transform.Find("Name").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI message = dialogBox.transform.Find("Text").GetComponent<TextMeshProUGUI>();

      
        while (dialog.Count > 0) {
            message.text = "";

            DialogMessage dialogMessage = dialog.Dequeue();
            charName.text = dialogMessage.charName;
            img.sprite = dialogMessage.charImage;

            foreach (char c in dialogMessage.message.ToCharArray()) {

                message.text += c;
                yield return null;
            }

            while (!Input.GetKeyDown(KeyCode.Space)) {

                yield return null;
            }
        }
        displaying = false;
        dialogBox.gameObject.SetActive(false);
        DialogEnd.Raise();
    }
}
