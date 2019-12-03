using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cyclopes : Boss {

    private enum CyclopeState { idle, walking, throwing, shooting,stagger};

    public GameObject rockPrefab;
    public GameObject laserPrefab;
    public float laserTimeout = 3f;
    private LaserBeam laser;

    Transform throwPoint;
    Transform shotPoint;



    CyclopeState state;

    public override void Start() {
        base.Start();
        throwPoint = transform.Find("ThrowPoint");
        shotPoint = transform.Find("ShotPoint");

        GameObject laserInstance = Instantiate(laserPrefab, transform.position, Quaternion.identity, shotPoint);
        laser = laserInstance.GetComponent<LaserBeam>();

        state = CyclopeState.idle;
    }

   public override void Update() {

        base.Update();

        Vector2 direction = getDirectionToPlayer();
        if (Input.GetKeyDown(KeyCode.Space)) {
          
            myAnimator.SetFloat("throwX", direction.x);
            myAnimator.SetFloat("throwY", direction.y);
            myAnimator.SetTrigger("throw");
        }

        if (Input.GetKeyDown(KeyCode.K)) {

            myAnimator.SetFloat("moveX", direction.x);
            myAnimator.SetFloat("moveY", direction.y);
            myAnimator.SetBool("shooting", true);
            myAnimator.SetTrigger("prepareShoot");
        }


    }


    public void lasershot() {

        if (state != CyclopeState.shooting) {
            StartCoroutine(laserShotCo());
            Debug.Log("Lasershot");
        }

      
    }


    private IEnumerator laserShotCo() {

        float elapsedTime = 0;
        state = CyclopeState.shooting;
        yield return null;
        laser.transform.position = shotPoint.position;
        laser.shot(getDirectionToPlayer());

        while(elapsedTime < laserTimeout) {

            elapsedTime += Time.deltaTime;
            yield return null;
        }

       yield return StartCoroutine(laser.dissapear());

        myAnimator.SetBool("shooting", false);
        state = CyclopeState.idle;

       
    }

    public void handler() {

        if(state != CyclopeState.throwing) {
            StartCoroutine(throwRockCo());
        }


    }

    public override void onTransferEnter() {
        throw new System.NotImplementedException();
    }

    public override void onTransferLeave() {
        throw new System.NotImplementedException();
    }



    public IEnumerator throwRockCo() {

            state = CyclopeState.throwing;
            yield return null;
          
            Vector3 throwPos = throwPoint.position;

            GameObject instance = Instantiate(rockPrefab, throwPos, Quaternion.identity, null);
            Rock r = instance.GetComponent<Rock>();

            r.shot(player.getCollisionCenterPoint());
            
           yield return new WaitForSeconds(0.2f);
            state = CyclopeState.idle;

    }
}
