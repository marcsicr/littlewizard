using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrap : MonoBehaviour
{
    public int damage = 5;
    private Animator myAnimator;
    private Player player;

    private void Awake() {
        myAnimator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if (other.CompareTag("Player")) {


          
            myAnimator.SetBool("close", true);
            player.OnGetKicked(damage);
            player.removeShield();
            StartCoroutine(openCo());
        }
    }

    IEnumerator openCo() {

        yield return new WaitForSeconds(0.3f);
        myAnimator.SetBool("close", false);
     
    }
}
