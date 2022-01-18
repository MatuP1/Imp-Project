using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static float volume = 1f;
    private AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update() {
        audioSource.volume = volume;
    }
    public void SetVolume (float vol) {
        volume = vol;
    }
}
