using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class SpellsBook : MonoBehaviour{

    SpellIndex boltIndex;
    SpellIndex shieldIndex;
    SpellIndex rayIndex;

    public GameObject pageBolt;
    public GameObject pageShield;
    public GameObject rayPage;

    public SpellLevelBar barBolt;
    public SpellLevelBar barShield;
    public SpellLevelBar barRay;




    private void Awake() {

        boltIndex = transform.Find("BoltIndex").GetComponent<SpellIndex>();
        shieldIndex = transform.Find("ShieldIndex").GetComponent<SpellIndex>();
        rayIndex = transform.Find("RayIndex").GetComponent<SpellIndex>();

    }

    private void OnEnable() {
        refreshUI();
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(boltIndex.gameObject);
    }

    private void refreshUI() {

        barBolt.setLevel(LevelManager.Instance.boltLevel);
        barShield.setLevel(LevelManager.Instance.shieldLevel);
        barRay.setLevel(LevelManager.Instance.rayLevel);
    }

    public void showSpell(Spell spell) {

        if (spell == Spell.BOLT) {
            boltIndex.setSelected(true);
            barBolt.setLevel(LevelManager.Instance.boltLevel);
            pageBolt.SetActive(true);

            pageShield.SetActive(false);
            rayPage.SetActive(false);

            shieldIndex.setSelected(false);
            rayIndex.setSelected(false);
            return;
        }

        if (spell == Spell.SHIELD) {

            boltIndex.setSelected(false);
            shieldIndex.setSelected(true);
            barShield.setLevel(LevelManager.Instance.shieldLevel);
            rayIndex.setSelected(false);

            pageBolt.SetActive(false);
            pageShield.SetActive(true);
            rayPage.SetActive(false);

            return;
        }

        if (spell == Spell.RANGE_ATTACK) {

            boltIndex.setSelected(false);
            shieldIndex.setSelected(false);
            rayIndex.setSelected(true);
            barRay.setLevel(LevelManager.Instance.rayLevel);

            pageBolt.SetActive(false);
            pageShield.SetActive(false);
            rayPage.SetActive(true);


            return;
        }

        Debug.Log("ShowSpell SpellNone");
    }

}
