using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlantState { IDLE,ATTACKING,TELEPORTING};
public class Plant : Enemy
{
    PlantState state;
    public GameObject bullet;
    public Vector2[] teleportPoints;
    public float teleportInterval = 2f;
    private float nextTeleport;
    private Material materiaL;
    private int currentPosIndex;
    BoxCollider2D myCollider;
   
    public void Awake() {
        materiaL = gameObject.GetComponent<SpriteRenderer>().material;
        myCollider = gameObject.GetComponent<BoxCollider2D>();
    }
    //private float nextAttackAvailable;
    public override void OnGetKicked(int attack) {
         
        HP -= attack;
        if(HP <= 0) {
            Destroy(gameObject);
        }
        bar.updateBar(HP);
        
    }

    // Start is called before the first frame update
    public override void Start()
    {
        //base.Start();
        if(bullet == null) {
            Debug.Log("Plant Error: Missing bullet");
        }

        state = PlantState.IDLE;
        player = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<Player>();

        gameObject.tag = TAG;
     

        base.nextAttackAvailable = Time.time + attackInterval;
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
        
        GameObject bulletUp = Instantiate(bullet, new Vector3(transform.position.x, transform.position.y + 0.9f, transform.position.z), Quaternion.identity);
        GameObject bulletUpR = Instantiate(bullet, new Vector3(transform.position.x+0.8f, transform.position.y + 0.9f, transform.position.z), Quaternion.identity);
        GameObject bulletUpL = Instantiate(bullet, new Vector3(transform.position.x - 0.8f, transform.position.y + 0.9f, transform.position.z), Quaternion.identity);
        GameObject bulletDown = Instantiate(bullet, new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), Quaternion.identity);
        GameObject bulletDownL = Instantiate(bullet, new Vector3(transform.position.x-0.8f, transform.position.y - 1f, transform.position.z), Quaternion.identity);
        GameObject bulletDownR = Instantiate(bullet, new Vector3(transform.position.x+0.8f, transform.position.y - 1f, transform.position.z), Quaternion.identity);
        
        GameObject bulletLeft = Instantiate(bullet, new Vector3(transform.position.x - 0.8f, transform.position.y, transform.position.z), Quaternion.identity);
        GameObject bulletRight = Instantiate(bullet, new Vector3(transform.position.x + 0.8f, transform.position.y, transform.position.z), Quaternion.identity);

        float duration = 0.5f;
        float maxOutlineWidth = 0.0032f;
        Vector4 color = new Vector4 (1f,0.8f,0f,1f);
        materiaL.SetVector("_OutlineColor", color);
        materiaL.SetFloat("_Brightness", 1f);
        for (float t = 0f; t < duration; t += Time.deltaTime) {
            float normalizedTime = t / duration;

            float width = Mathf.Lerp(0, maxOutlineWidth, normalizedTime);
            materiaL.SetFloat("_Width", width);
            yield return null;
        }
        materiaL.SetFloat("_Width", maxOutlineWidth);

        bulletUp.GetComponent<PlantBullet>().shot(Vector2.up);
        bulletDown.GetComponent<PlantBullet>().shot(Vector2.down);
        bulletLeft.GetComponent<PlantBullet>().shot(Vector2.left);
        bulletRight.GetComponent<PlantBullet>().shot(Vector2.right);
        bulletUpR.GetComponent<PlantBullet>().shot(new Vector2(1,1));
        bulletUpL.GetComponent<PlantBullet>().shot(new Vector2(-1,1));
        bulletDownR.GetComponent<PlantBullet>().shot(new Vector2(1,-1));
        bulletDownL.GetComponent<PlantBullet>().shot(new Vector2(-1,-1));
        
   

        for (float t = 0f; t < duration; t += Time.deltaTime) {
            float normalizedTime = t / duration;

            float width = Mathf.Lerp(maxOutlineWidth,0, normalizedTime);
            materiaL.SetFloat("_Width", width);
            yield return null;
        }

        materiaL.SetFloat("_Width", 0);
        materiaL.SetFloat("_Brightness", 0);
        state = PlantState.IDLE;

    }

    // Update is called once per frame
     public override void Update()
    {
        switch (state) {

            case PlantState.IDLE:
                if (attackAtempt()) {
                    state = PlantState.ATTACKING;
                }

                if (teleportAtempt()) {
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
            materiaL.SetVector("_Color", new Vector4(c.r, c.g, c.b, c.a));
             
            yield return null;
        }

        if(teleportPoints.Length > 0) { //Pick another position different from current
            int random;
            do {
                random = Random.Range(0, teleportPoints.Length - 1);
            } while (random == currentPosIndex);
            currentPosIndex = random;
        }
        
        transform.position = new Vector3(teleportPoints[currentPosIndex].x, teleportPoints[currentPosIndex].y, transform.position.z);
        yield return null;

        for (float t = 0f; t < duration; t += Time.deltaTime) {
            float normalizedTime = t / duration;

            c = Color.Lerp(whiteAlpha, whiteFull, normalizedTime);
            materiaL.SetVector("_Color", new Vector4(c.r, c.g, c.b, c.a));

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
