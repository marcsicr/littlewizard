using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightStartTrigger : MonoBehaviour
{
    public GameObject bossBarPrefab;
    public Sprite bossFace;
    BossStatusBar bar;
    private void OnEnable() {

        bar = Instantiate(bossBarPrefab, transform.Find("/UILayout"), false).GetComponent<BossStatusBar>();
        bar.setFace(bossFace);
    }
}
