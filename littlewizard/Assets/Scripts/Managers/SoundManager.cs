using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum SONG {FOREST, CAVE, CASTLE,BOSS,HOUSE}
public class SoundManager : MonoBehaviour {

    public AudioClip forestSong;
    public AudioClip caveSong;
    public AudioClip castleSong;
    public AudioClip houseSong;
    public AudioClip bossBattleSong;

    public AudioClip gameOverClip;

    public AudioClip winClip;
    public AudioClip winBigClip;

    public AudioMixer audioMixer;


    private AudioSource musicSource;
    private AudioSource effectsSource;
    private AudioSource playerVoice;

    

    private float volume;
    public static SoundManager Instance { get; private set; }
    
    private void Awake() {

        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            musicSource = createAudioSource("Music", true);
            effectsSource = createAudioSource("Effects", false);
            playerVoice = createAudioSource("Voice", false);
        } else {
            Destroy(gameObject);
        }
    }

    private void Start() {

        float savedVolume = PlayerPrefs.GetFloat("volume", 1);
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(savedVolume) * 20);
    }

    public void changeSong(SONG song) {

        musicSource.clip = getAudioClip(song);
        musicSource.Play();
    }
    

    private AudioClip getAudioClip(SONG song) {

        switch (song) {
            case SONG.FOREST:
                return forestSong;
            case SONG.CAVE:
                return caveSong;
            case SONG.CASTLE:
                return castleSong;
            case SONG.BOSS:
                return bossBattleSong;

            case SONG.HOUSE:
                return houseSong;

            default:
                return null;
        }
    }



    public void playEffect(AudioClip effect) {
        effectsSource.PlayOneShot(effect);
    }

    public void playVoice(AudioClip voice) {
        playerVoice.PlayOneShot(voice);
    }

    private AudioSource createAudioSource(string name,bool loop) {

        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups(name)[0];
        audioSource.loop = loop;

        return audioSource;
    }


    public void onTransferEnter( ) {
        
        StartCoroutine(fadeOutCo(0.3f));
    }

    public void onTransferLeave() {

        StopAllCoroutines();
        StartCoroutine(fadeInCo(volume,0.5f));
    }

    public IEnumerator theEndCo() {

        effectsSource.Stop();
        musicSource.Stop();
        yield return null;

        musicSource.clip = winBigClip;
        musicSource.PlayOneShot(winBigClip);
        yield return new WaitForSeconds(winBigClip.length/2);
    }
    private IEnumerator fadeOutCo(float fadeTime) {
      
            float startVolume = musicSource.volume;
           volume = startVolume;

            while (musicSource.volume > 0) {
                   musicSource.volume -= startVolume * Time.deltaTime / fadeTime;

                yield return null;
            }


        //musicSource.Stop();
        yield return StartCoroutine(fadeInCo(startVolume, fadeTime));
       // musicSource.Play();
           //musicSource.volume = startVolume;
        
    }

    private IEnumerator fadeInCo(float volume,float fadeTime) {

        float startVolume = musicSource.volume;

        while (musicSource.volume < volume) {
            musicSource.volume += startVolume * Time.deltaTime / fadeTime;

            yield return null;
        }

        //musicSource.Stop();
        //musicSource.volume = startVolume
    }


     public void bossFightEnter() {
        changeSong(SONG.BOSS);
     }

    public void onBossDefeated() {

        StartCoroutine(bossDefeatedCo());
    }

   
    public void onGameOver() {

        StartCoroutine(GameOverCo());
    }

    private IEnumerator GameOverCo() {

        musicSource.Stop();
        musicSource.clip = gameOverClip;
        musicSource.Play();
        yield return new WaitForSeconds(gameOverClip.length);
        musicSource.Stop();
    }

    private IEnumerator bossDefeatedCo() {

        yield return new WaitForSeconds(0.2f);
        musicSource.Stop();
        effectsSource.PlayOneShot(winClip);
        yield return new WaitForSeconds(winClip.length);
        changeSong(SONG.CAVE);
            //(SONG.CAVE);
    }

    public void setMasterVolume(float sliderValue) {

        PlayerPrefs.SetFloat("volume", sliderValue);
        Debug.Log(sliderValue);
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
    }

    public float getMasterVolume() {

        return PlayerPrefs.GetFloat("volume",1);
    }

}
