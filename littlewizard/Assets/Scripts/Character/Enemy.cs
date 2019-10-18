﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Enemy : Character{

    [HideInInspector]
    public static readonly string ENEMY_TAG = "Enemy"; //This tag is defined first on inspector
    
    protected Transform target;

    public float HP = 100;
    public float chaseRadius = 5f;
    public float attackRadius = 2f;
    protected Vector2 spawnLocation;

  
    public override void Start() {
        base.Start();
    
        target = GameObject.FindGameObjectWithTag("Player").transform;
        gameObject.tag = ENEMY_TAG;
        
        spawnLocation = transform.position;

        if (debugCharacter)
        {
            debugStart();
        }
        
    }

    
    private void debugStart(){
        DrawCircle(chaseRadius, Color.cyan);
        DrawCircle(attackRadius, Color.red);
    }

    private void DrawCircle(float radius, Color color) {

        GameObject lineContainer = new GameObject();

        lineContainer.transform.SetParent(this.transform);
  
        LineRenderer lineRenderer = lineContainer.AddComponent<LineRenderer>();
        lineRenderer.transform.SetParent(this.transform);

        lineRenderer.useWorldSpace = false;
        lineRenderer.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
        Debug.Log(lineRenderer.transform.position.x.ToString() + lineRenderer.transform.position.y.ToString());
        
        
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.sortingLayerName = "Player";
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
       
        int segments = 128;
        lineRenderer.positionCount = segments + 1;

        float deltaTheta = (float)(2.0 * Mathf.PI) / segments;
        float theta = 0f;

        for (int i = 0; i < segments + 1; i++) {
            float x = radius * Mathf.Cos(theta);
            float y = radius * Mathf.Sin(theta);
            Vector3 pos = new Vector3(x, y, transform.position.z);
            lineRenderer.SetPosition(i, pos);
            theta += deltaTheta;
        }
    }
    protected bool isPlayerInChaseRadius(){
       return  Vector3.Distance(target.position, transform.position) <= chaseRadius;
    }

    protected bool isPlayerInAttackRadius() {
        return Vector3.Distance(target.position, transform.position) <= attackRadius;
    }

   
}
