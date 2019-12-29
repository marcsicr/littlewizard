using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pushable : MonoBehaviour {


    public AudioClip pushClip;
    private AudioSource audioSource;
    private Rigidbody2D myRigidbody;
    private Vector2 startPosition;

   

    public void Start() {

        myRigidbody = GetComponent<Rigidbody2D>();
        startPosition = myRigidbody.position;
        audioSource = GetComponent<AudioSource>();
    }

    

    private void OnCollisionEnter2D(Collision2D collision) {

        

        if (collision.gameObject.CompareTag("Enemy")) {
            myRigidbody.velocity = Vector2.zero;
            myRigidbody.angularVelocity = 0;

            
        }else if (collision.gameObject.CompareTag("Bullet")) {
            
            StartCoroutine(stopHitCo());
        } else {

            audioSource.Play();
        }

        
    }


    private void OnCollisionExit2D(Collision2D collision) {


        myRigidbody.velocity = Vector2.zero;
        myRigidbody.angularVelocity = 0;

        audioSource.Stop();
    }

    private IEnumerator stopHitCo() { 

        yield return new WaitForSeconds(0.3f);
        myRigidbody.velocity = Vector2.zero;
        myRigidbody.angularVelocity = 0;
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if (other.CompareTag("Enemy")) {
            myRigidbody.isKinematic = true;
            //myRigidbody.bodyType = RigidbodyType2D.Static;
        }
    }


    private void OnTriggerExit2D(Collider2D other) {

        if (other.CompareTag("Enemy")) {
            myRigidbody.isKinematic = false;
            //myRigidbody.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    

    private IEnumerator resetPositionCo() {

        float duration = 1f;
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        Color start = renderer.color;
        Color end = start;
        end.a = 0;

        for (float t = 0f; t < duration/2; t += Time.deltaTime) {
            float normalizedTime = t / duration;

            renderer.color = Color.Lerp(start, end, normalizedTime);
            yield return null;
        }

        myRigidbody.position = startPosition;

        for (float t = 0f; t < duration/2; t += Time.deltaTime) {
            float normalizedTime = t / duration;

            renderer.color = Color.Lerp(end, start, normalizedTime);
            yield return null;
        }

        renderer.color = start;

    }

    public void restartPosition() {
        if(myRigidbody.position != startPosition) {
            StartCoroutine(resetPositionCo());
        }
    }
}
