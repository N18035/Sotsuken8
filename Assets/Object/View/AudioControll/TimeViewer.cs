using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ken
{
    public class TimeViewer : MonoBehaviour
    {
        [SerializeField] AudioSource _audioSource;
        [SerializeField] Text _audioTime;
        [SerializeField] Text _musicTime;


        void AudioTIme(string s){
            _audioTime.text = s;
        }

        //Musicエンジンの時間
        void MusicTime(string s){
            _musicTime.text = s;
        }


        void Update(){
            if(_audioSource.clip == null) return;

            //再生時間
            AudioTIme(_audioSource.time.ToString("F2"));

            if(Music.Just.IsNull())   MusicTime("---");
            else                     MusicTime(Music.Just.ToString());
        }

    }
}

