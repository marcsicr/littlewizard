using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBolt : LinearBullet {

    private bool isCameraFollowing = false;

    public override void onCollision(Vector2 collisionPoint) {

        Vector2 hitDirection = collisionPoint - (Vector2)transform.position;
        myAnimator.SetFloat("moveX", hitDirection.x);
        myAnimator.SetFloat("moveY", hitDirection.y);

        myAnimator.SetTrigger("explode");
        transform.Find("trail").gameObject.SetActive(false);
        Destroy(GetComponent<Collider2D>());
        activeSpeed = 0;
        collided = true;

        if (isCameraFollowing) {
            StartCoroutine(resetCameraWithDelay(0.8f));
        }

        Destroy(gameObject,2);
    }

    public override void setShotHeight(int height) {

        base.setShotHeight(height);
        TrailRenderer r = transform.Find("trail").gameObject.GetComponent<TrailRenderer>();
        r.sortingOrder = height;

    }

    public override void shot(Vector2 direction) {


        //Debug.Log("Bullet start:" + transform.position + "Bullet direction:" + direction);

        gameObject.SetActive(true);
        transform.Find("trail").gameObject.SetActive(true);
        myAnimator.SetFloat("moveX", direction.x);
        myAnimator.SetFloat("moveY", direction.y);

        base.shot(direction);
    }

    public void setCameraTarget() {
        Camera.main.GetComponent<CameraPlayer>().changeTartget(transform);
        isCameraFollowing = true;
    }

    private IEnumerator resetCameraWithDelay(float delay) {

        yield return new WaitForSeconds(delay);
        Camera.main.GetComponent<CameraPlayer>().resetTarget();
    }
}
