using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineDirector : MonoBehaviour
{
    public Player p;
    public GameObject magician;
    public Vector2 endDirection;

    PlayableDirector director;
    
    private RuntimeAnimatorController playerRAC;
    public Animator playerAnimator;

    Vector3 lastpos;
    Vector3 magicianPos;
    bool paused = false;
    bool stopped = false;
    bool flag = false;
    bool bugFix = false;
    bool converted = false;
    private void Awake() {
        playerAnimator.SetFloat("moveY", -1);
        director = GetComponent<PlayableDirector>();
        director.stopped += OnDirectorStoped;
    }
    void OnEnable()
    {
       playerRAC = playerAnimator.runtimeAnimatorController;
       playerAnimator.runtimeAnimatorController = null;

     
    }

    public void play() {

        flag = true;
        director.Play();

    }

   void OnDirectorStoped(PlayableDirector director) {

        Debug.Log("YAYY STOPPEPD");
        stopped = true;
    }

    // Update is called once per frame
    void Update(){
        if (!paused) {
            lastpos = p.transform.position;
            magicianPos = magician.transform.position;
        }
      
       /* Debug.Log(p.transform.position);
        if (director.state != PlayState.Playing && !bugFix && flag) {

            Debug.Log("Fixed shit");
            bugFix = true;
            playerAnimator.runtimeAnimatorController = playerRAC;
            p.GetComponent<BoxCollider2D>().enabled = true;
        }*/

        

    }


   public void resume() {

        director.playableGraph.GetRootPlayable(0).SetSpeed(1);
        p.onTransferLeave();
        paused = false;
    }
    public void pause() {

        //pausedPos = p.transform.position;
        
        p.onTransferEnter();
        director.playableGraph.GetRootPlayable(0).SetSpeed(0);

         paused = true;
         converted = false;
    }

    private void LateUpdate() {

        if (director.state != PlayState.Playing && !bugFix && flag) {

            Debug.Log("Fixed shit");
            bugFix = true;
            playerAnimator.runtimeAnimatorController = playerRAC;
            magician.GetComponent<Animator>().runtimeAnimatorController = null;
            //p.GetComponent<BoxCollider2D>().enabled = true;
        }

        if (stopped && flag) {
            p.transform.position = lastpos;
            playerAnimator.SetFloat("moveX", endDirection.x);
            playerAnimator.SetFloat("moveY", endDirection.y);

            magician.transform.position = magicianPos;
            Destroy(gameObject);
        }

        /*if (paused && !converted) {
            converted = true;

            StartCoroutine(resetCamera());
           //Camera.main.transform.position = lastpos;
          
        }*/



    }

    private IEnumerator resetCamera() {

        while (paused) {
            //Camera.main.transform.position = lastpos;
            p.transform.position = lastpos;

            yield return new WaitForSeconds(1f);
        }
      

    }
}
