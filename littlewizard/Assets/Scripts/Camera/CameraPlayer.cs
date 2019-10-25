using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
    public Transform target;
    public float smoothing;
    public Vector2 topLeft;
    public Vector2 bottomRight;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetPosition = target.position + new Vector3(0, 0, -1);

        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothing);
        smoothPosition.x = Mathf.Clamp(smoothPosition.x, topLeft.x,bottomRight.x);
        smoothPosition.y = Mathf.Clamp(smoothPosition.y, bottomRight.y, topLeft.y);
        transform.position = smoothPosition;  
    }

    public void updateBoundaries(Vector2 topLeft, Vector2 bottomRight) {

        this.topLeft = topLeft;
        this.bottomRight = bottomRight;
    }
}
