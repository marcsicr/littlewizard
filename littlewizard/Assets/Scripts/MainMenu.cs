using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public GameObject fadePanel;
    public GameObject startBtn;
    public GameObject soundBtn;
    public GameObject volumePanelPrefab;

    

    void Start()
    {
        Debug.Log("Started");
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(startBtn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGame() {

        StartCoroutine(startGameCo());
        
    }

    private IEnumerator startGameCo() {

        Image img = fadePanel.GetComponent<Image>();

        
        Color start = new Color32(1, 1, 1, 0);
        Color end = new Color32(1, 1, 1, 255);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);
        asyncLoad.allowSceneActivation = false;

        float time = SoundManager.Instance.playEnterGameEffect();

        float fadeDuration = time / 2;
        for (float t = 0f; t < fadeDuration; t += Time.deltaTime) {
            float normalizedTime = t / fadeDuration;

            img.color = Color.Lerp(start, end, normalizedTime);
            yield return null;
        }

        yield return new WaitForSeconds(time / 2);
        img.color = end;
       
        //yield return new WaitForSeconds(time);

        asyncLoad.allowSceneActivation = true;

    }

    public void showVolumePanel() {

        
        VolumePanel panel = Instantiate(volumePanelPrefab,transform.parent).GetComponent<VolumePanel>();

        panel.setBackgroundPanel(gameObject,soundBtn);



    }

    public void exitGame() {

        Application.Quit();
    }
}
