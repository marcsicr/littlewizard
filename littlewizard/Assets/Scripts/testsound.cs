using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testsound : MonoBehaviour
{
    public AudioClip m1;
    public AudioClip m2;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) {

           // SoundManager.Instance.setSong(m1);
        }
        if (Input.GetKeyDown(KeyCode.N)) {

            //SoundManager.Instance.setSong(m2);
        }
    }
}
