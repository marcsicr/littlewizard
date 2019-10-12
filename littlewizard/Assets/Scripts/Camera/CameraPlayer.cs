using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
    public Transform target;
    public float smoothing;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetPosition = target.position + new Vector3(0, 0, -10);
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothing);
        transform.position = smoothPosition;  
    }
}
