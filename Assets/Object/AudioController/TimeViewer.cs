using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Ken
{
    public class TimeViewer : MonoBehaviour
    {
        [SerializeField] AudioSource _audioSource;
        [SerializeField] Text _audioTime;

        //[SerializeField] Text _musicTime;
        [SerializeField]private List<Text> _musicTime;

        void AudioTIme(string s){
            _audioTime.text = s;
        }


        void Update(){
            if(_audioSource.clip == null) return;

            //再生時間
            AudioTIme(_audioSource.time.ToString("F2"));

            if(Music.Just.IsNull()){
                _musicTime[0].text = "---";
                _musicTime[1].text = "-";
                _musicTime[2].text = "-";
            }
            else
            {
                _musicTime[0].text = (Music.Just.Bar + 1).ToString();
                _musicTime[1].text = (Music.Just.Beat + 1).ToString();
                _musicTime[2].text = (Music.Just.Unit + 1).ToString();
            }

        }

    }
}

