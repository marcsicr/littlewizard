using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RangeAttk : MonoBehaviour
{
    private struct EnemyRay {
        public Enemy enemy;
        public LineRenderer line;
        public GameObject jointPoint;
        

        public EnemyRay(Enemy enemy, LineRenderer line,GameObject jointPoint) { this.enemy = enemy; this.line = line; this.jointPoint = jointPoint; }

    }

    public float attackDuration;
    public int attackPower;
    public float rayWidth;
    public float updateInterval;
    public Sprite jointPoint;
    public float radius;
    public Material lineMaterial;

    private int ENEMY_LAYER;
    private float nextUpdate;
    
    private List<EnemyRay> lines;
    private static float maxDeviationFactor = 1f;
    private float devationFactor = 0;
    private static float deviationStep = 0.10f;

    private SpriteRenderer spriteRenderer;
    private bool fired;
    
    // Update is called once per frame
    private void Awake() {

        lines = new List<EnemyRay>();
        ENEMY_LAYER = 1 << LayerMask.NameToLayer("Enemy");
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

   

    public void shot(Vector2 direction) {

        gameObject.SetActive(true);
        transform.position += new Vector3(direction.x, direction.y, 0);
        nextUpdate = Time.time;
        //Obtenir els enemics que es troben dins del cercle
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, radius, ENEMY_LAYER);

        foreach (Collider2D e in enemies) {
            Enemy enemy = e.GetComponent<Enemy>();
            CreateLine(enemy);
        }

        if(enemies.Length <= 0) {
            Debug.Log("No enemies");
        }
        
        fired = true;

        StartCoroutine(lifeTimeCo());
    }

    private IEnumerator lifeTimeCo() {
        float dissapearTime = 0.2f;
        Color whiteAlpha = new Color(1f, 1f, 1f, 0);
        Color whiteFull = new Color(1f, 1f, 1f, 1f);
        foreach (EnemyRay e in lines) {

            e.enemy.OnGetKicked(attackPower);
        }

        yield return new WaitForSeconds(attackDuration);

        for (float t = 0f; t < dissapearTime; t += Time.deltaTime) {
            float normalizedTime = t / dissapearTime;

            foreach (EnemyRay e in lines) {

                Color c = Color.Lerp(whiteFull, whiteAlpha, normalizedTime);
                if (e.enemy != null) {   
                    e.line.startColor = c;
                    e.jointPoint.GetComponent<SpriteRenderer>().color = c;
                }
               
                this.spriteRenderer.color = c;
                
            }

            yield return null;
           
        }


        foreach (EnemyRay e in lines) {

            Destroy(e.jointPoint);
        }
        Destroy(gameObject);
    }

    void Update(){

        if (fired) {
            UpdatLines();
        }
       
    }

    void UpdatLines() {

        if (Time.time < nextUpdate)
            return;
        
        foreach(EnemyRay r in lines) {
            
            if(r.enemy!= null) {
                UpdateEnemyRay(r, devationFactor);
            } else {

                Destroy(r.line); //If enemy died
            }
           
        }
        nextUpdate = Time.time + updateInterval;
        devationFactor = Mathf.Clamp(devationFactor + deviationStep, 0,maxDeviationFactor);
    }

    void UpdateEnemyRay(EnemyRay r, float devationFactor) {


        float deviation = devationFactor; //0.5f;
        Vector3 enemyPos = r.enemy.transform.position;
        Vector3 lastPoint = transform.position;
        int i = 1;
        
        r.line.SetPosition(0, transform.position);//Ray starts at this gameobject position
        while (Vector3.Distance(lastPoint, r.enemy.transform.position) > 0.5f) {//while the last point of the ray is not close enough 

            r.line.positionCount = i + 1;
            Vector3 fwd = Vector3.Normalize(r.enemy.transform.position - lastPoint);//Get direction from the last point of the ray to the enemy
            
            fwd = RandomDeviation(fwd, deviation); // Get deviation point
            fwd += lastPoint; // Next point of the ray is lastPoint + desviation
            r.line.SetPosition(i, fwd);
            i++;
            lastPoint = fwd;
        }
        r.line.positionCount = i + 1;
        r.line.SetPosition(i, enemyPos);
    }

    void CreateLine(Enemy e) {


        GameObject lineContainer = new GameObject();
        lineContainer.name = "RayLine";

        lineContainer.transform.SetParent(this.transform);

        LineRenderer lineRenderer = lineContainer.AddComponent<LineRenderer>();
        lineRenderer.transform.SetParent(this.transform);

        lineRenderer.transform.SetPositionAndRotation(transform.position, Quaternion.identity);

        lineRenderer.material = lineMaterial;
        lineRenderer.sortingLayerName = "Player";
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;
        lineRenderer.startWidth = rayWidth;
        lineRenderer.endWidth = rayWidth;

    

        GameObject point = new GameObject();
        point.name = "RayPoint";
        SpriteRenderer sr = point.AddComponent<SpriteRenderer>();
        sr.sprite = jointPoint;
        sr.sortingLayerID = SortingLayer.NameToID("Player");
        sr.transform.SetParent(e.transform,false);
        sr.sortingOrder = 1;
        sr.transform.localPosition = Vector3.zero;

        lines.Add(new EnemyRay(e, lineRenderer,point));
    }


    /**
     * @param vec3: initial point
     * @param devation: deviation factor
     * 
     * @return initial point deviated randomly
     */
    private Vector3 RandomDeviation(Vector3 vec3, float devation) {
        float minLength = 0.4f;
        float maxLength = 0.8f;
       
        vec3 += new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0) * devation;
        vec3.Normalize();

        vec3 *= Random.Range(minLength, maxLength); // Make arc deviation length more random
        return vec3;
    }

}
