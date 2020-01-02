using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public SONG menuSong;
    void Start()
    {

        SoundManager.Instance.changeSong(menuSong);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
