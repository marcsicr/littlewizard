using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cyclopes : Boss {


    public ObservableInteger BossHP;
    public ObservableFloat BossSP;

    public float discoverDistance;
    public GameObject rockPrefab;
    public GameObject laserPrefab;

    [HideInInspector]
    public Transform eyePoint { get; private set; }
    [HideInInspector]
    public LaserBeam laser { get; private set; }
    [HideInInspector]
    public bool isLaserStateSheduled = false;
    public float laserDuration;
    public float SPRecoverTime = 16f;
    public bool isShooting = false;

    public bool isDead = false;
       
    //-------------------------------------------------

    public float stunTimeout = 8;
    private float currentStunTimeOut;
    private bool stunning = false;
    [HideInInspector]
    public bool isStunReady = false;

    //----------------------------------------------
    Transform throwPoint;
    private bool throwing = false;
    
    private bool isOutlineEffectEnabled = false;

    public Vector3[] patrolPoints { get; private set; }

    [HideInInspector]
    public int patrolIndex;

    
    

    public override void Start() {
        base.Start();

        BossHP.initialValue = HP;
        BossHP.reset();

        BossSP.initialValue = SPRecoverTime;
        BossSP.reset();
        

        Transform pointsGroup = transform.Find("PatrolPoints");
        patrolPoints = new Vector3[pointsGroup.childCount];
        patrolIndex = 0;
        Debug.Log(pointsGroup.GetChild(0).position);

        //Add each patrol point in the vector in the same order in hierary on the editor
        for (int i = 0; i < pointsGroup.childCount; i++) {
            Transform child = pointsGroup.GetChild(i);
            patrolPoints[child.GetSiblingIndex()] = child.position;

        }

        Destroy(pointsGroup.gameObject);
        //StartCoroutine(FsmCo());

        throwPoint = transform.Find("ThrowPoint");
        eyePoint = transform.Find("ShotPoint");

        GameObject laserInstance = Instantiate(laserPrefab, transform.position, Quaternion.identity, eyePoint);
        laser = laserInstance.GetComponent<LaserBeam>();
       
        currentStunTimeOut = stunTimeout;


    }

    public override void OnGetKicked(int attack) {
        base.OnGetKicked(attack);

     
        if (BossHP.getRunTimeValue() > 0) {
            if (BossHP.getRunTimeValue() > attack) {
                BossHP.UpdateValue(BossHP.getRunTimeValue() - attack);
            } else {
                BossHP.UpdateValue(0);
                BossSP.UpdateValue(0);
            }
        }

        if (BossHP.getRunTimeValue() <= 0) {

            GetComponent<Collider2D>().enabled = false;
            isDead = true;
            myAnimator.SetTrigger("die");
            //Destroy(gameObject);
        } else {

            kickAnimation = true;
        }

    }



    public void resetStunTimeOut() {
        currentStunTimeOut = stunTimeout;
        isStunReady = false;
    }

    public override void Update() {
        base.Update();

        currentStunTimeOut -= Time.deltaTime;

        if (!isShooting && !isDead) {
            float currentSP = Mathf.Clamp(BossSP.getRunTimeValue() + Time.deltaTime, 0, BossSP.initialValue);
            BossSP.UpdateValue(currentSP);
        }

        if(currentStunTimeOut <= 0) {
            isStunReady = true;
        }

        if (Input.GetKeyDown(KeyCode.N)) {

            startStun();
        }
    }

    public Vector2 getSpawnLocation() {

        return spawnLocation;
    }
    public override void onTransferEnter() {

        myAnimator.SetBool("stop", true);
    }

    public override void onTransferLeave() {
        myAnimator.SetBool("stop", false);
    }


    public void handler() {

        if (!throwing) {
            StartCoroutine(Rock());
        }


    }

    public void startStun() {

        if (!stunning) {
            stunning = true;
            StartCoroutine(StunPlayerCo());
        }
       
    }

    private IEnumerator StunPlayerCo() {

     
        float stuntDuration = 0.5f;

        CameraPlayer  camera = Camera.main.GetComponent<CameraPlayer>();
        player.stun(stuntDuration * 2);
        yield return StartCoroutine(camera.shakeCo(stuntDuration, 0.3f));

        //player.stun(0.5f * 2);
        resetStunTimeOut();

        stunning = false;
    }


    public IEnumerator Rock() {

        throwing = true;
        //yield return null;

        Vector3 throwPos = throwPoint.position;

        GameObject instance = Instantiate(rockPrefab, throwPos, Quaternion.identity, null);
        Rock r = instance.GetComponent<Rock>();

        r.shot(player.getCollisionCenterPoint());

        yield return new WaitForSeconds(0.2f);
        throwing = false;

    }

    public void outLineEffectEnabled(bool isEnabled) {

        if (isEnabled) {
            isOutlineEffectEnabled = true;
            StartCoroutine(OutlineCo());
        } else {
            isOutlineEffectEnabled = false;
        }
           
    }

    public IEnumerator OutlineCo() {

        Material material = base.mat;
        Color color = new Color32(244,40,40,255);
        float maxOutlineWidth = 0.002f;
        float duration = 0.5f;
        mat.SetVector("_OutlineColor", color);
        mat.SetFloat("_Brightness", 1f);

        while (isOutlineEffectEnabled) {
            for (float t = 0f; t < duration && isOutlineEffectEnabled; t += Time.deltaTime) {
                float normalizedTime = t / duration;

                float width = Mathf.Lerp(0, maxOutlineWidth, normalizedTime);
                material.SetFloat("_Width", width);
                yield return null;
            }

            for (float t = 0f; t < duration && isOutlineEffectEnabled; t += Time.deltaTime) {
                float normalizedTime = t / duration;

                float width = Mathf.Lerp(maxOutlineWidth,0, normalizedTime);
                material.SetFloat("_Width", width);
                yield return null;
            }

            material.SetFloat("_Width", 0);
        }
        
    }

    public void sheduleLaserAttack() {
        
        isLaserStateSheduled = true;
        float delay = (float)Random.Range(2, 8);
        Debug.Log("Scheduled in " + delay);
        Invoke("goToLaserAttackPoint", delay);
    }

    private void goToLaserAttackPoint() {

        if (!isDead) {
            myAnimator.SetTrigger("prepareToShot");
        }
       
    }

   // private IEnumerator outLine

}
//    private IEnumerator FsmCo() {

//        float time = 2f;
//        state = CyclopeState.Patrol;

//        while (true) {

//            time -= Time.deltaTime;
//            if (Time.time <= 0) {
//                yield return StartCoroutine(state.ToString());
//                time = 5f;
//            }

//            yield return null;
//        }

//    }

//    private IEnumerator Patrol() {

//        int targetIndex = 0;

//        myAnimator.SetBool("walk", true);
//        while (getDistanceToPlayer() > 3f ) {
//            yield return new WaitForFixedUpdate();

//            if (Vector3.Distance (transform.position,patrolPoints[targetIndex])<= 1f) {

//                //Update target
//                if(targetIndex < patrolPoints.Length-1) {
//                    targetIndex++;
//                } else {
//                    targetIndex = 0;
//                }
//            } 

//            Vector3 step = Vector3.MoveTowards(transform.position, patrolPoints[targetIndex], speed * Time.fixedDeltaTime);
//            myRigidBody.MovePosition(step);

//            Vector2 dir = (step - transform.position).normalized;
//            myAnimator.SetFloat("moveX", dir.x);
//            myAnimator.SetFloat("moveY", dir.y);

//        }

//        state = CyclopeState.Laser;
//    }

//    private IEnumerator Laser() {


//        while (Vector3.Distance(transform.position, spawnLocation) > 1f) {

//            if(Vector3.Distance(transform.position, spawnLocation) > 1f) {
//                myAnimator.SetBool("walk", true);
//                while (Vector3.Distance(transform.position, spawnLocation) > 1f) {
//                    Vector3 step = Vector3.MoveTowards(transform.position, spawnLocation, speed * Time.fixedDeltaTime);
//                    Vector3 dir = (step - transform.position).normalized;
//                    myAnimator.SetFloat("moveX", dir.x);
//                    myAnimator.SetFloat("moveY", dir.y);
//                    myRigidBody.MovePosition(step);
//                    yield return null;
//                }

//                myAnimator.SetFloat("moveX", getDirectionToPlayer().x);
//                myAnimator.SetFloat("moveY", getDirectionToPlayer().x);
//                myAnimator.SetBool("shooting", true);
//                myAnimator.SetTrigger("prepareShoot");

//                yield return new WaitForSeconds(1);
//                myAnimator.SetBool("walk", false);
//            }



//            yield return null;
//        }


//    }

//    public Vector2 getSpawnLocation() {

//        return spawnLocation;
//    }
//   //public override void Update() {

//   //     base.Update();

//   //     ///* Vector2 playerDirection = getDirectionToPlayer();
//   //     // myAnimator.SetFloat("moveX", playerDirection.x);
//   //     // myAnimator.SetFloat("moveY", playerDirection.y);*/

//   //     // if (Input.GetKeyDown(KeyCode.L)) {

//   //     //     myAnimator.SetFloat("throwX", playerDirection.x);
//   //     //     myAnimator.SetFloat("throwY", playerDirection.y);
//   //     //     myAnimator.SetTrigger("throw");
//   //     // }

//   //     if (Input.GetKeyDown(KeyCode.K)) {

//   //         myAnimator.SetFloat("moveX", getDirectionToPlayer().x);
//   //         myAnimator.SetFloat("moveY", getDirectionToPlayer().x);
//   //         myAnimator.SetBool("shooting", true);
//   //         myAnimator.SetTrigger("prepareShoot");
//   //     }


//   // }

//    //public void updateState() {

//    //    StopAllCoroutines();


//    //    if(state == CyclopeState.idle) {
//    //        state = CyclopeState.walking;
//    //        StartCoroutine(PatrolingCo());
//    //        myAnimator.SetBool("walk", true);
//    //        timeOut = 5;
//    //        return;
//    //    }

//    //    if(state == CyclopeState.walking) {
//    //        state = CyclopeState.idle;
//    //        myAnimator.SetBool("walk", false);
//    //        StartCoroutine(IdleCo());
//    //        timeOut = 1;
//    //        return;
//    //    }
//    //}

//    //private IEnumerator IdleCo() {

//    //    while (true) {
//    //        if (Vector3.Distance(player.transform.position, transform.position) > 8)
//    //            myRigidBody.MovePosition((Vector2)transform.position + getDirectionToPlayer());
//    //        yield return null;
//    //    }
//    // }






//    public void lasershot() {

//        if (state != CyclopeState.Laser) {
//            StartCoroutine(laserShotCo());
//            Debug.Log("Lasershot");
//        }


//    }


//    private IEnumerator laserShotCo() {

//        float elapsedTime = 0;
//        state = CyclopeState.Laser;
//        yield return null;
//        laser.transform.position = eyePoint.position;
//        laser.shot(getDirectionToPlayer());

//        while(elapsedTime < laserTimeout) {

//            elapsedTime += Time.deltaTime;
//            yield return null;
//        }

//       yield return StartCoroutine(laser.dissapear());

//        myAnimator.SetBool("shooting", false);
//        state = CyclopeState.Idle;


//    }

//    public void handler() {

//        if(state != CyclopeState.Rock) {
//            StartCoroutine(Rock());
//        }


//    }





//    public IEnumerator Rock() {

//            state = CyclopeState.Rock;
//            //yield return null;

//            Vector3 throwPos = throwPoint.position;

//            GameObject instance = Instantiate(rockPrefab, throwPos, Quaternion.identity, null);
//            Rock r = instance.GetComponent<Rock>();

//            r.shot(player.getCollisionCenterPoint());

//           yield return new WaitForSeconds(0.2f);
//            state = CyclopeState.Patrol;

//    }
//}
