using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ken.DanceView
{
    public class TimeViewer : MonoBehaviour
    {
        [SerializeField] Text _musicTime;
        [SerializeField] Text _audioTime;

        public void AudioTIme(string s){
            _audioTime.text = s;
        }

        public void MusicTime(string s){
            _musicTime.text = s;
        }

    }
}

