using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Enemy
{
    public GameObject magicBullet;
    public override void OnGetKicked(int attack) {
        base.OnGetKicked(attack);
        this.HP -= attack;
        if(this.HP <= 0) {

            Destroy(gameObject);
        }

        bar.updateBar(HP);

    }

    // Start is called before the first frame update
   public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame


    private void shot() {

        Transform shootPoint = transform.Find("ShotPoint").gameObject.transform;
        //Transform shootPoint = gameObject.GetComponentInChildren<Transform>();
        if (shootPoint == null)
            Debug.Log("Error: No shootPoint");

      //  Debug.Log("MagicBullet shot");
        Instantiate(magicBullet, shootPoint.transform.position,Quaternion.identity);

        
    }

    protected override void attackAction() {
        shot();
    }
}
