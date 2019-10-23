using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomShot : MonoBehaviour
{
    
    public float speed = 2f;
    public float maxTime = 4f;
    private static int damage = 10;

    private float lifeTime = 0;
    private Rigidbody2D myRigidBody;
    private Vector2 direction;
    
    
    

    // Start is called before the first frame update
    void Start()
    {
        
        myRigidBody = GetComponent<Rigidbody2D>();

        GameObject target = GameObject.FindGameObjectWithTag("Player");
        direction = target.transform.position - gameObject.transform.position;
        direction.Normalize();

        //Rotate magic bullet
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    }

    // Update is called once per frame
    private void FixedUpdate() {

        lifeTime += Time.fixedDeltaTime;
        if(lifeTime >= maxTime) {
            explode();
        } else {
            myRigidBody.velocity = direction * speed;
        }
    }

    public void explode() {

        Destroy(gameObject);
    }
    public void OnTriggerEnter2D(Collider2D other) {


        if(other.gameObject.tag == "Player") {

            Player player = other.GetComponent<Player>();
            explode();
            player.OnGetKicked(damage);
            
        }
         
    }
}
