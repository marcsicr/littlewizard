using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBullet : MonoBehaviour
{
    
    public float speed = 15f;

    private float activeSpeed = 0;
    public float maxTime = 15f;
    private static int damage = 10;
    //public ParticleSystem explosion;
    //public ParticleSystem bulletSparks;

    private float lifeTime = 0;
    private Rigidbody2D myRigidBody;
    private Vector2 direction;
    private Renderer rend;
    
    
    

    // Start is called before the first frame update
    void Start()
    {
        
        myRigidBody = GetComponent<Rigidbody2D>();

     
        rend = gameObject.GetComponent<Renderer>();

    }

    // Update is called once per frame
    private void FixedUpdate() {

        lifeTime += Time.fixedDeltaTime;
        if(lifeTime >= maxTime) {
            Destroy(gameObject);
        } else {
            myRigidBody.velocity = direction * activeSpeed;
        }
    }

    public IEnumerator explodeCo() { 
       

       // ParticleSystem particle = Instantiate(explosion,gameObject.transform.position,Quaternion.identity,gameObject.transform);
        //particle.loop = false; //OLD API?

       // bulletSparks.Stop();
        //particle.Play();
        speed = 0;
        //rend.enabled = false;
        // yield return new WaitForSeconds(2f);
        yield return null;
        Destroy(gameObject);
    }


    public void shot(Vector2 direction) {

        //Set direction && Make bullet visible && set speed
        this.gameObject.SetActive(true);
        this.direction = direction;
        this.activeSpeed = speed;

    }


    public void OnTriggerEnter2D(Collider2D other) {


        if(other.gameObject.tag == "Player") {

            Player player = other.GetComponent<Player>();
            StartCoroutine(explodeCo());
            player.OnGetKicked(damage);
            
        }
         
    }
}
