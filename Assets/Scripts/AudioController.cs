using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    public AudioMixer masterMixer;
    public void SetVolume (float vol) {
        masterMixer.SetFloat("MasterVolume",Mathf.Log10(vol) * 20);
    }
}
