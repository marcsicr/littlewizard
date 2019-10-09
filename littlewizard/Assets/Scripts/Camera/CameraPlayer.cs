using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
    public GameObject target;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        

    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPos = new Vector3(
            target.transform.position.x, 
            target.transform.position.y, 
            transform.position.z);

        transform.position = newPos;
    }
}
