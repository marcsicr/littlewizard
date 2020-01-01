using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShotDirection {DOWN,UP,LEFT,RIGHT};
public class WallShooter : MonoBehaviour
{
    private Player player;
    public GameObject linearBulletPrefab;
    public ShotDirection direction;

    public float offset;
    public float shotRate; 
    private Vector2 _direction;
    public float bulletSpeed;
    void Start(){

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        switch (direction) {

            case ShotDirection.DOWN: {
                    _direction = Vector2.down;
                    break;
            }
            case ShotDirection.UP: {
                    _direction = Vector2.up;
                    break;
                }
            case ShotDirection.LEFT: {
                    _direction = Vector2.left;
                    break;
                }
            case ShotDirection.RIGHT: {
                    _direction = Vector2.right;
                    break;
            }
        }

        InvokeRepeating("shot", offset, shotRate);
        
    }


    private void shot() {

        if (Vector3.Distance(player.transform.position, transform.position) > 15)
            return;

        LinearBullet bullet = Instantiate(linearBulletPrefab, transform.position, Quaternion.identity, null).GetComponent<LinearBullet>();
        int height = LevelManager.Instance.getTileLevel(transform.position);
        Vector3 scale = bullet.transform.localScale;

        bullet.speed = bulletSpeed;
        bullet.transform.localScale = scale * 1.2f;
        bullet.setShotHeight(height);
        bullet.shot(_direction);

    }

}
