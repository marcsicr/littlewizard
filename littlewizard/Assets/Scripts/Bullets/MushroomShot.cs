using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomShot : Bullet
{
    
  
    public ParticleSystem explosion;
    public ParticleSystem bulletSparks;
    private Renderer rend;
    

    // Start is called before the first frame update
    void Start()
    {


        /* GameObject target = GameObject.FindGameObjectWithTag("Player");
         direction = target.transform.position - gameObject.transform.position;
         direction.Normalize();

         //Rotate magic bullet
         float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
         transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);*/

        rend = gameObject.GetComponent<Renderer>();

    }

    // Update is called once per frame

    public IEnumerator explodeCo() { 
       

        ParticleSystem explosionParticles = Instantiate(explosion,gameObject.transform.position,Quaternion.identity,gameObject.transform);
        //particle.loop = false; //OLD API?

        bulletSparks.Stop();
        explosionParticles.Play();
        speed = 0;
        rend.enabled = false;
        yield return new WaitForSeconds(2f);
        
        Destroy(gameObject);
    }

    public override void shot(Vector2 direction) {

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        gameObject.SetActive(true);
        startMoving(direction);
        Destroy(gameObject, lifetime);
    }

    public override void onCollision() {
        StartCoroutine(explodeCo());

    }
}
