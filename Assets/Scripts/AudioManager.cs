using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip;
    public float volume = 0.5f;
    public float playTime;
    public bool loopSound;

    private void Start() {
        audioSource.volume = volume;
        if(loopSound) playTime = audioClip.length;
        if(playTime > 0) StartCoroutine("PlayForSeconds");
        else audioSource.PlayOneShot(audioClip);
    }

    IEnumerator PlayForSeconds() {
        while(true) {
            audioSource.PlayOneShot(audioClip);
            yield return new WaitForSeconds(playTime);
            audioSource.Stop();
        }
    }
}
