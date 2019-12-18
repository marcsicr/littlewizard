using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLevelTransfer : MonoBehaviour
{
    public FadeImg fadeImg;
    private void Start() {
        GameManager.Instance.loadFirstLevelAsync();

    }
    private void OnTriggerEnter2D(Collider2D other) {

        if (other.CompareTag(Player.TAG)) {

            StartCoroutine(changeLevelCo());
        }
    }

    private IEnumerator changeLevelCo() {

        fadeImg.FadeOut();
        yield return new WaitForSeconds(0.5f);

        GameManager.Instance.goToFirstLevel();

    }
}
