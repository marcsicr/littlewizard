using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingElement : MonoBehaviour {
    public enum MovingDirection { Horizontal, Vertical };

    public bool flip;
    public bool paused;

    private float timer;
    public MovingDirection direction;
   

    public float speed;
    public float distance;
    private float moveOrientation = 1;

    private Vector2 startPos;

    void Start() {
        
        if (flip) {
            moveOrientation = -1;
        }
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update() {

        if (!paused) {

            timer += Time.deltaTime;
            if (direction == MovingDirection.Horizontal) {
                transform.position = new Vector3(startPos.x + Mathf.PingPong(timer * speed, distance) * moveOrientation, transform.position.y, transform.position.z);
            } else if (direction == MovingDirection.Vertical) {
                transform.position = new Vector3(transform.position.x, startPos.y + Mathf.PingPong(timer * speed, distance) * moveOrientation, transform.position.z);
            }
        }
       
       // Vector2 newPos = startPos + Mathf.Sin(Time.time * speed) * distance * vDirection * moveOrientation;
        //transform.position = newPos;
    }

    public void isPaused(bool isPaused) {
        paused = isPaused;
    }
}
