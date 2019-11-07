using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltTrail : MonoBehaviour
{
    TrailRenderer trailRenderer;
    void Start()
    {
        Vector3[] positions = { new Vector3(0, 0, 0), new Vector3(9, 0, 0) };
        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.sortingOrder = 10;
        trailRenderer.SetPositions(positions);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
