using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

namespace Ken.Setting
{

    public class BeatSoundPresenter : MonoBehaviour{
        [SerializeField] Ken.Beat.BeatSound _beatSound;
        [SerializeField] Dropdown _dropdown;
        [SerializeField] Toggle up1;
        [SerializeField] Toggle upOnly1;
        [SerializeField] Button button;
        [SerializeField] Text text;
        [SerializeField] GameObject setting;

        [SerializeField] AudioSource seSource1;
        [SerializeField] AudioSource seSource2;

        Dictionary<int, string> ClipNameDictionary = new Dictionary<int, string>()
        {
            {0, "電子音"},
            {1, "拍手"},
            {2, "音無し"},
        };

        [SerializeField] AudioClip[] SEClips = new AudioClip[3];
        [SerializeField] AudioClip[] SEClipsUP = new AudioClip[3];


        void Start(){
            _dropdown.onValueChanged.AsObservable()
            .Subscribe(_ => Change())
            .AddTo(this);

            up1.onValueChanged.AsObservable()
            .Subscribe(_ => Change())
            .AddTo(this);

            upOnly1.onValueChanged.AsObservable()
            .Subscribe(_ => Change())
            .AddTo(this);

            button.onClick.AsObservable()
            .Where(_ => !setting.activeSelf)
            .Subscribe(_ => setting.SetActive(true))
            .AddTo(this);
        }

        void Change()
        {
            int v = _dropdown.value;
            seSource1.clip =  SEClips[v];
            seSource2.clip =  SEClipsUP[v];
            Ken.Beat.BeatSoundData data = new Beat.BeatSoundData(v,up1.isOn,upOnly1.isOn);
            _beatSound.SetBeatSoundSetting(data);

            //UI
            text.text = ClipNameDictionary[_dropdown.value];
            setting.SetActive(false);
        }
    }
}
