using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransfer : MonoBehaviour
{
    public string sceneOut;
    // Start is called before the first frame update
    void Start()  {
        
    }


    public void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {

            Debug.Log("Rom transfer exit");
            AsyncOperation asyncOp = SceneManager.LoadSceneAsync(sceneOut);
        }
    }
}
