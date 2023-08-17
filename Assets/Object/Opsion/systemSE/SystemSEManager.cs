using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemSEManager : Singleton<SystemSEManager>
{
    [SerializeField] AudioSource _audioSource;
    
    public List<AudioClip> SE = new List<AudioClip>(5);

    public void Good(){
        _audioSource.volume = 1f;
        _audioSource.PlayOneShot(SE[0]);
    }

    public void Better(){
        _audioSource.volume = 1f;
        _audioSource.PlayOneShot(SE[1]);
    }
}
