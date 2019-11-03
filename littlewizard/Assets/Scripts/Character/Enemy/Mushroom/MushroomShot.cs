using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomShot : MonoBehaviour
{
    
    public float speed = 2f;
    public float maxTime = 4f;
    private static int damage = 20;
    public ParticleSystem explosion;
    public ParticleSystem bulletSparks;

    private float lifeTime = 0;
    private Rigidbody2D myRigidBody;
    private Vector2 direction;
    private Renderer rend;
    
    
    

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

        rend = gameObject.GetComponent<Renderer>();

    }

    // Update is called once per frame
    private void FixedUpdate() {

        lifeTime += Time.fixedDeltaTime;
        if(lifeTime >= maxTime) {
            Destroy(gameObject);
        } else {
            myRigidBody.velocity = direction * speed;
        }
    }

    public IEnumerator explodeCo() { 
       

        ParticleSystem particle = Instantiate(explosion,gameObject.transform.position,Quaternion.identity,gameObject.transform);
        //particle.loop = false; //OLD API?

        bulletSparks.Stop();
        particle.Play();
        speed = 0;
        rend.enabled = false;
        yield return new WaitForSeconds(2f);
        
        Destroy(gameObject);
    }
    public void OnTriggerEnter2D(Collider2D other) {


        if(other.gameObject.tag == "Player") {

            Player player = other.GetComponent<Player>();
            StartCoroutine(explodeCo());
            player.OnGetKicked(damage);
            
        }
         
    }
}
