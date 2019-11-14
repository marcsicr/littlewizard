using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pushable : MonoBehaviour {

    [SerializeField] private Rigidbody2D myRigidbody;
    [SerializeField] private float pushWait;
    bool isPushed = false;

    private Vector2 pushDirection;

    private void Awake() {
        
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    /*private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player") && !isPushed) {
           
            Player p = other.gameObject.GetComponent<Player>();
            Debug.Log("Pushing direction:"+p.movingDirection());
            pushDirection = p.movingDirection();
            StartCoroutine(pushCo());
            isPushed = true;

           // Debug.Log("Is Pushed");
        }
    }*/

    /*private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            isPushed = false;
            StopAllCoroutines();
        }
    }*/

    private void OnCollisionExit2D(Collision2D collision) {

        myRigidbody.velocity = Vector2.zero;
        myRigidbody.angularVelocity = 0;
    }
    private IEnumerator pushCo() {
        yield return new WaitForSeconds(pushWait);


        Push(pushDirection);
    }

    void Push(Vector3 pushDirection) {
        myRigidbody.MovePosition(transform.position + pushDirection);
    }
}
