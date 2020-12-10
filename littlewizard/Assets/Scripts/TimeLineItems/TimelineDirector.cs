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

    //private RuntimeAnimatorController otherRAC;
    private Animator otherAnimator;


    Vector3 lastpos;
    Vector3 otherpos;
    bool paused = false;
    bool stopped = false;
    bool isTimelineStarted = false;
    bool bugFix = false;
    
    private void Awake() {
        
        director = GetComponent<PlayableDirector>();
        director.stopped += OnDirectorStoped;

        otherAnimator = otherActor.GetComponent<Animator>();
    }
  
    public void play() {

        playerRAC = playerAnimator.runtimeAnimatorController;
        playerAnimator.runtimeAnimatorController = null;

        isTimelineStarted = true;
        
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

            Debug.Log("Fixed");
            bugFix = true;
            playerAnimator.runtimeAnimatorController = playerRAC;
       
        }*/

        

    }


   public void resume() {

        if (isTimelineStarted) {
            director.playableGraph.GetRootPlayable(0).SetSpeed(1);
            player.onTransferLeave();
            paused = false;
        }
       
    }
    public void pause() {

        //pausedPos = p.transform.position;

        if (isTimelineStarted) {
            Debug.Log("Paused");
            player.onTransferEnter();
            director.playableGraph.GetRootPlayable(0).SetSpeed(0);
            paused = true;
        }
       

       
    
    }

    private void LateUpdate() {

        if (director.state != PlayState.Playing && !bugFix && isTimelineStarted) {

            bugFix = true;
            playerAnimator.runtimeAnimatorController = playerRAC;
            //otherAnimator.runtimeAnimatorController = otherRAC;
        }

        if (stopped && isTimelineStarted) {
            player.transform.position = lastpos;
            playerAnimator.SetFloat("moveX", playerEndDirection.x);
            playerAnimator.SetFloat("moveY", playerEndDirection.y);

            otherActor.transform.position = otherpos;
            endOfTimeline.Raise();
            Destroy(gameObject);
        }
    }
}
