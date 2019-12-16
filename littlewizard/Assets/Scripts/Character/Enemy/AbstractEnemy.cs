using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEnemy : Character {

    [HideInInspector]
    public static readonly string TAG = "Enemy"; //This tag must be defined first on inspector

    protected Vector2 spawnLocation;
    protected Player player;
    public int HP = 100;
    
    public override void Start() {
        base.Start();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        gameObject.tag = TAG;
        spawnLocation = transform.position;
    }

    public override void OnGetKicked(int attack) {
        kickAnimation = true;
    }

    public float distanceFromPlayer() {
        return Vector3.Distance(player.transform.position, transform.position);
    }

    public Transform getPlayerTransform() {
        return this.player.transform;
    }

    public Vector2 getDirectionToPlayer() {
        Vector3 direction = player.transform.position - transform.position;
        return new Vector2(direction.x, direction.y).normalized;
    }

    public float getDistanceToPlayer() {

        return Vector3.Distance(transform.position, player.transform.position);
    }

    public void resetSpeed() {
        myRigidBody.velocity = Vector2.zero;
        myRigidBody.angularVelocity = 0;
    }
}
