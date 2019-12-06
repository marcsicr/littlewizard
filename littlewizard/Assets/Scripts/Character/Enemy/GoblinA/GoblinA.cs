using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinA : Enemy
{
  private enum GolbinAState { patrol,attack,idle}

    private Vector3[] patrolPoints;
    public GameObject arrowPrefab;
    private bool shooting = false;

   
    void Awake() {

        List<Vector3> listPatrolPoints = new List<Vector3>();
        Transform pointsHolder = transform.Find("PatrolPoints");
        Debug.Log("Childs count" + pointsHolder.childCount);
        Transform[] transformPoints = pointsHolder.GetComponentsInChildren<Transform>();

        foreach(Transform t in transformPoints) {

            if (t.position != pointsHolder.position) {
                listPatrolPoints.Add(t.position);
            }
        }

       patrolPoints =  listPatrolPoints.ToArray();

        Destroy(pointsHolder.gameObject);
        
    }

    public override void OnGetKicked(int attack) {
        base.OnGetKicked(attack);
        this.HP -= attack;
        if (this.HP <= 0) {

            Destroy(gameObject);
        }

        bar.updateBar(HP);
    }

    protected override void attackAction() {

        Vector2 direction = getDirectionToPlayer();
        myAnimator.SetFloat("moveX",direction.x);
        myAnimator.SetFloat("moveY", direction.y);
        myAnimator.SetTrigger("shot");


        //StartCoroutine(waitShotCo());
      
    }

    //Event function called on animation 
    public void ArrowShot() {


        if (!shooting) {

            Debug.Log("Arrow shot");
            StartCoroutine(ArrowShotCo());
        }
         


    }
    public IEnumerator ArrowShotCo() {

        shooting = true;
        yield return new WaitForSeconds(0.12f);

        Vector2 direction = getDirectionToPlayer();
        GameObject arrow = Instantiate(arrowPrefab, new Vector3(transform.position.x + direction.x, transform.position.y + direction.y, transform.position.z), Quaternion.identity);
        Physics2D.IgnoreCollision(arrow.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        arrow.GetComponent<Arrow>().shot(player.getCollisionCenterPoint());

        shooting = false;
    }


    public Vector3[] getPatrolPoints() {

        return this.patrolPoints;
    }
}
