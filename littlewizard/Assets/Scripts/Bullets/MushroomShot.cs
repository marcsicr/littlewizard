using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomShot : LinearBullet
{
    
  
    public ParticleSystem explosion;
    public ParticleSystem bulletSparks;
    private Renderer rend;

    private bool exploding = false;
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
       

        bulletSparks.Stop();
        explosionParticles.Play();
        activeSpeed = 0;
        rend.enabled = false;
        yield return new WaitForSeconds(0.5f);
        
        Destroy(gameObject);
    }

    public override void shot(Vector2 point) {

        base.shot(point);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

       
    }

    public override void onCollision(Vector2 collisionPoint) {

        if (!exploding) {
            exploding = true;
            StartCoroutine(explodeCo());
        }
       

    }
}
