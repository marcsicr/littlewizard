using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlantState { IDLE,ATTACKING,TELEPORTING};
public class Plant : Enemy
{
    PlantState state;
    public GameObject bullet;
  
    public float teleportInterval = 4.5f;
    private float nextTeleport;
    public float teleportRadius;
    //private Material materiaL;
    private int currentPosIndex;
    BoxCollider2D myCollider;
   
    public void Awake() {
        //materiaL = gameObject.GetComponent<SpriteRenderer>().material;
        myCollider = gameObject.GetComponent<BoxCollider2D>();
    }
    //private float nextAttackAvailable;
    public override void OnGetKicked(int attack) {

       
        StartCoroutine(KickEffectCo());

        HP -= attack;
        if(HP <= 0) {
            Destroy(gameObject,0.1f);

        }
        bar.updateBar(HP);
        
    }

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        if(bullet == null) {
            Debug.Log("Plant Error: Missing bullet");
        }

        state = PlantState.IDLE;
        player = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<Player>();

        gameObject.tag = TAG;
     

        base.nextAttackAvailable = Time.time + attackInterval+Random.Range(0,1);
        nextTeleport = Time.time + teleportInterval;
        currentPosIndex = 0;
        spawnLocation = transform.position;

        if (debugCharacter) {
            base.debugStart();
        }

        bar = gameObject.GetComponentInChildren<EnemyBar>();
        if (bar == null) {
            Debug.Log("No Enemy HP bar component found");
        }


    }

    public override bool isAttackReady() {
        return (Time.time > nextAttackAvailable) && state == PlantState.IDLE && isPlayerInAttackRadius();
    }

    protected override void attackAction() {
        
            StartCoroutine(plantShotCo());
    }
    //Instantiate bullets and shot
    private IEnumerator plantShotCo() {
        
        PlantBullet bulletUp = Instantiate(bullet, new Vector3(transform.position.x, transform.position.y + 0.9f, transform.position.z), Quaternion.identity).GetComponent<PlantBullet>();
        PlantBullet bulletUpR = Instantiate(bullet, new Vector3(transform.position.x+0.8f, transform.position.y + 0.9f, transform.position.z), Quaternion.identity).GetComponent<PlantBullet>();
        PlantBullet bulletUpL = Instantiate(bullet, new Vector3(transform.position.x - 0.8f, transform.position.y + 0.9f, transform.position.z), Quaternion.identity).GetComponent<PlantBullet>();
        PlantBullet bulletDown = Instantiate(bullet, new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), Quaternion.identity).GetComponent<PlantBullet>();
        PlantBullet bulletDownL = Instantiate(bullet, new Vector3(transform.position.x-0.8f, transform.position.y - 1f, transform.position.z), Quaternion.identity).GetComponent<PlantBullet>();
        PlantBullet bulletDownR = Instantiate(bullet, new Vector3(transform.position.x+0.8f, transform.position.y - 1f, transform.position.z), Quaternion.identity).GetComponent<PlantBullet>();

        PlantBullet bulletLeft = Instantiate(bullet, new Vector3(transform.position.x - 0.8f, transform.position.y, transform.position.z), Quaternion.identity).GetComponent<PlantBullet>();
        PlantBullet bulletRight = Instantiate(bullet, new Vector3(transform.position.x + 0.8f, transform.position.y, transform.position.z), Quaternion.identity).GetComponent<PlantBullet>();

        float duration = 0.5f;
        float maxOutlineWidth = 0.0032f;
        Vector4 color = new Vector4 (1f,0.8f,0f,1f);
        mat.SetVector("_OutlineColor", color);
        mat.SetFloat("_Brightness", 1f);
        for (float t = 0f; t < duration; t += Time.deltaTime) {
            float normalizedTime = t / duration;

            float width = Mathf.Lerp(0, maxOutlineWidth, normalizedTime);
            mat.SetFloat("_Width", width);
            yield return null;
        }
        mat.SetFloat("_Width", maxOutlineWidth);

        int height = getMapHeight();

        bulletUp.setShotHeight(height);
        bulletDown.setShotHeight(height);
        bulletLeft.setShotHeight(height);
        bulletRight.setShotHeight(height);
        bulletUpR.setShotHeight(height);
        bulletUpL.setShotHeight(height);
        bulletDownR.setShotHeight(height);
        bulletDownL.setShotHeight(height);

        bulletUp.shot(Vector2.up);
        bulletDown.shot(Vector2.down);
        bulletLeft.shot(Vector2.left);
        bulletRight.shot(Vector2.right);
        bulletUpR.shot(new Vector2(1,1));
        bulletUpL.shot(new Vector2(-1,1));
        bulletDownR.shot(new Vector2(1,-1));
        bulletDownL.shot(new Vector2(-1,-1));
        
   

        for (float t = 0f; t < duration; t += Time.deltaTime) {
            float normalizedTime = t / duration;

            float width = Mathf.Lerp(maxOutlineWidth,0, normalizedTime);
            mat.SetFloat("_Width", width);
            yield return null;
        }

        mat.SetFloat("_Width", 0);
        mat.SetFloat("_Brightness", 0);
        state = PlantState.IDLE;

    }

    // Update is called once per frame
     public override void Update()
    {

       
            switch (state) {

                case PlantState.IDLE:
                    if (attackAtempt() && isPlayerInAttackRadius()) {
                        state = PlantState.ATTACKING;
                    }

                    if (teleportAtempt() && isPlayerInChaseRadius()) {
                        state = PlantState.TELEPORTING;
                    }

                    break;

                case PlantState.TELEPORTING:

                    break;

                case PlantState.ATTACKING:
                    break;

            }          
    }

    

    public bool teleportAtempt() {

        if (Time.time > nextTeleport) {
            //mat.SetFloat("_Width", 0);
            //mat.SetFloat("_Brightness", 0);
            StopAllCoroutines();
            mat.SetFloat("_Width", 0);
            mat.SetFloat("_FlashAmount", 0);
            mat.SetFloat("_Brightness", 0);
            StartCoroutine(TeletransportCo());
            return true;
        }
        return false;
    }
    public IEnumerator TeletransportCo() {

       
        bar.transform.parent.gameObject.SetActive(false);
        myCollider.enabled = false;
        
        Color whiteAlpha = new Color(1f, 1f, 1f, 0);
        Color whiteFull = new Color(1f, 1f, 1f, 1f);
           float duration = 0.5f; // 0.5 seconds
        Color c;
        for (float t = 0f; t < duration; t += Time.deltaTime) {
            float normalizedTime = t / duration;

            c = Color.Lerp( whiteFull,whiteAlpha,normalizedTime);
            mat.SetVector("_Color", new Vector4(c.r, c.g, c.b, c.a));
             
            yield return null;
        }

        transform.position = spawnLocation + Random.insideUnitCircle * teleportRadius;
        
        
        yield return null;

        for (float t = 0f; t < duration; t += Time.deltaTime) {
            float normalizedTime = t / duration;

            c = Color.Lerp(whiteAlpha, whiteFull, normalizedTime);
            mat.SetVector("_Color", new Vector4(c.r, c.g, c.b, c.a));

            yield return null;
        }

        myCollider.enabled = true;
        bar.transform.parent.gameObject.SetActive(true);
        nextTeleport = Time.time + teleportInterval;
        state = PlantState.IDLE;
    }

    public override void onGameOver() {
        

    }

}
