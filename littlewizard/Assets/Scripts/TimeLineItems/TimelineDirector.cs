using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineDirector : MonoBehaviour
{
    public Player player;
    public GameObject otherActor;
    public Vector2 playerEndDirection;
    public Vector2 otherEndDirection;
    public Signal endOfTimeline;
    public Signal startOfTimeLine;
    

    PlayableDirector director;
    
    private RuntimeAnimatorController playerRAC;
    public Animator playerAnimator;

    private RuntimeAnimatorController otherRAC;
    private Animator otherAnimator;


    Vector3 lastpos;
    Vector3 otherpos;
    bool paused = false;
    bool stopped = false;
    bool flag = false;
    bool bugFix = false;
    
    private void Awake() {
        
        director = GetComponent<PlayableDirector>();
        director.stopped += OnDirectorStoped;

        otherAnimator = otherActor.GetComponent<Animator>();
    }
    void OnEnable()
    {
      //playerRAC = playerAnimator.runtimeAnimatorController;
       //playerAnimator.runtimeAnimatorController = null;

     
    }

    public void play() {

        playerRAC = playerAnimator.runtimeAnimatorController;
        //otherRAC = otherAnimator.runtimeAnimatorController;

        //otherAnimator.runtimeAnimatorController = null;
        playerAnimator.runtimeAnimatorController = null;

        flag = true;
        
        director.Play();

    }

   void OnDirectorStoped(PlayableDirector director) {

        stopped = true;
    }

    // Update is called once per frame
    void Update(){
        if (!paused) {
            lastpos = player.transform.position;
            otherpos = otherActor.transform.position;
        }
      
       /* Debug.Log(p.transform.position);
        if (director.state != PlayState.Playing && !bugFix && flag) {

            Debug.Log("Fixed shit");
            bugFix = true;
            playerAnimator.runtimeAnimatorController = playerRAC;
       
        }*/

        

    }


   public void resume() {

        director.playableGraph.GetRootPlayable(0).SetSpeed(1);
        player.onTransferLeave();
        paused = false;
    }
    public void pause() {

        //pausedPos = p.transform.position;
        
        player.onTransferEnter();
        director.playableGraph.GetRootPlayable(0).SetSpeed(0);

         paused = true;
    
    }

    private void LateUpdate() {

        if (director.state != PlayState.Playing && !bugFix && flag) {

            
            bugFix = true;
            playerAnimator.runtimeAnimatorController = playerRAC;
            otherAnimator.runtimeAnimatorController = otherRAC;
            
            //otherActor.GetComponent<Animator>().runtimeAnimatorController = null;

 
        }

        if (stopped && flag) {
            player.transform.position = lastpos;
            playerAnimator.SetFloat("moveX", playerEndDirection.x);
            playerAnimator.SetFloat("moveY", playerEndDirection.y);


            otherActor.transform.position = otherpos;
            endOfTimeline.Raise();
            Destroy(gameObject);
        }


    }

    private IEnumerator resetCamera() {

        while (paused) {
            //Camera.main.transform.position = lastpos;
            player.transform.position = lastpos;

            yield return new WaitForSeconds(1f);
        }
      

    }
}
