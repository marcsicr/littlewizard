using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public void Start() {

        GetComponent<Slider>().value = SoundManager.Instance.getMasterVolume();
    }

    public void ChangeVolume(Slider slider) {
        SoundManager.Instance.setMasterVolume(slider.value);
    }
}
