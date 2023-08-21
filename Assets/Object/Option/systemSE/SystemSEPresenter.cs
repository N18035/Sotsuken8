using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemSEPresenter : Singleton<SystemSEPresenter>
{
    [SerializeField] AudioSource _audioSource;
    
    public List<AudioClip> SE = new List<AudioClip>();

    public void Good(){
        _audioSource.volume = 1f;
        _audioSource.PlayOneShot(SE[0]);
    }

    public void Better(){
        _audioSource.volume = 1f;
        _audioSource.PlayOneShot(SE[1]);
    }
}
