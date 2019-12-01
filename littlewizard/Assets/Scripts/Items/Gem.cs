using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : Item
{
    public GameObject levelPortalPrefab;
    public void Start() {

        LevelManager.Instance.registerGem(gameObject);
        
    }

    public override void onItemCollect(Player player) {
      
        LevelManager.Instance.addGemCaught(gameObject);

        if(LevelManager.Instance.gemsCaughtCount() == LevelManager.Instance.gemsOnLevel()) {

            Vector3 position = transform.Find("PortalSpawnPoint").position;
            GameObject instance = Instantiate(levelPortalPrefab, position, Quaternion.identity, null);
            instance.GetComponent<LevelPortal>().appear(0,true);

            
        }
      
    }

}
