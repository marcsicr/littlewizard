using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Enemy : Character{

    [HideInInspector]
    public static readonly string ENEMY_TAG = "Enemy"; //This tag must be defined first on inspector

 

    protected Player target;

    public int HP = 100;
    public float chaseRadius = 5f;
    public float attackRadius = 2f;
    public int attackPower = 10;
    public float minDistance = 2.5f;
    public float attackInterval = 2f; // Minium time between attacks
    protected float nextAttackAvailable;
    protected Vector2 spawnLocation;


    

    protected EnemyBar bar;
    public override void Start() {
        base.Start();

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        
        gameObject.tag = ENEMY_TAG;
       
        myRigidBody.useFullKinematicContacts = true;

        nextAttackAvailable = Time.time + attackInterval;

        spawnLocation = transform.position;

        if (debugCharacter)
        {
            debugStart();
        }

        bar = gameObject.GetComponentInChildren<EnemyBar>();
        if (bar == null) {
            Debug.Log("No Enemy HP bar component found");
        }

    }

    public override void onTransferEnter() {

        
        myAnimator.SetBool("freeze", true);
    }

    public override void onTransferLeave() {

        StartCoroutine(defrostCo());

    }

    private IEnumerator defrostCo() {

        yield return new WaitForSeconds(0.9f);
        myAnimator.SetBool("freeze", false);
    }

    public int getHP() {

        return this.HP;
    }

    public override void OnGetKicked(int attack) {
        kickAnimation = true;
    }

    protected void debugStart(){
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
    public bool isPlayerInChaseRadius(){
       return  Vector3.Distance(target.transform.position, transform.position) <= chaseRadius;
    }

    public bool isPlayerInAttackRadius() {
        return Vector3.Distance(target.transform.position, transform.position) <= attackRadius;
    }

    public float distanceFromPlayer() {

        return Vector3.Distance(target.transform.position, transform.position);
    }

    public Transform getTarget() {
        return this.target.transform;
    }
   
    public void move(Vector2 position) {
        myRigidBody.MovePosition(position);
    }

    public Vector2 getTargetDirection() {

        Vector3 direction = target.transform.position - transform.position;
        return new Vector2(direction.x, direction.y).normalized;

    }

    public void resetSpeed() {
        myRigidBody.velocity = Vector2.zero;
        myRigidBody.angularVelocity = 0;
    }

    protected abstract void attackAction();

    /*Try to initiate attack if attempt is successfull return true*/
    public bool attackAtempt() {

        if(isAttackReady()) {

            attackAction();
            nextAttackAvailable = Time.time + attackInterval;
            return true;
        }

        return false;
    }

    public virtual bool isAttackReady() {
        return Time.time > nextAttackAvailable;
    }

    public virtual void onGameOver() {

        myAnimator.SetTrigger("gameOver");
    }
    
}
