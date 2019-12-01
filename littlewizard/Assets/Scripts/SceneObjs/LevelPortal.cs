using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPortal : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other) {


        if (other.CompareTag("Player")) {

            Debug.Log("Disappear");

            Player player = other.gameObject.GetComponent<Player>();

            player.dissapear();

            GetComponent<Animator>().SetTrigger("disappear");

            StartCoroutine(goToNextLevelCo());
        }
    }

    public void disappear() {
        GetComponent<Animator>().SetTrigger("disappear");
    }

    public void appear(int Zorder,bool isExit) {

        if (isExit) {
            GetComponent<CircleCollider2D>().enabled = true;
        }

        
        GetComponent<SpriteRenderer>().sortingOrder = Zorder;
        GetComponent<Animator>().SetTrigger("appear");
    }

    private IEnumerator goToNextLevelCo() {

        yield return new WaitForSeconds(2);
        GameManager.Instance.goToNextLevel();
    }
}
