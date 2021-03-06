﻿using System.Collections;
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

        boltIndex = transform.Find("Indices/BoltIndex").GetComponent<SpellIndex>();
        shieldIndex = transform.Find("Indices/ShieldIndex").GetComponent<SpellIndex>();
        rayIndex = transform.Find("Indices/RayIndex").GetComponent<SpellIndex>();

    }

    private void OnEnable() {
        refreshUI();
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(boltIndex.gameObject);
    }

    private void refreshUI() {

        barBolt.setLevel(SpellsManager.Instance.boltLevel);
        barShield.setLevel(SpellsManager.Instance.shieldLevel);
        barRay.setLevel(SpellsManager.Instance.rayLevel);
    }

    public void showSpell(Spell spell) {

        if (spell == Spell.BOLT) {
            boltIndex.setSelected(true);
            barBolt.setLevel(SpellsManager.Instance.boltLevel);
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
            barShield.setLevel(SpellsManager.Instance.shieldLevel);
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
            barRay.setLevel(SpellsManager.Instance.rayLevel);

            pageBolt.SetActive(false);
            pageShield.SetActive(false);
            rayPage.SetActive(true);


            return;
        }

        Debug.Log("ShowSpell SpellNone");
    }

}
