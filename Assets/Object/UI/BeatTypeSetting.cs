using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

namespace Ken.Setting
{
    public class BeatTypeSetting : MonoBehaviour
    {

        [SerializeField] Toggle BeatView48;
        [SerializeField] Music music;
        public IObservable<Unit> OnSelectBeatType => _selectBeatType;
        private Subject<Unit> _selectBeatType = new Subject<Unit>();


        void Start(){
            ChangeBeatType(4);

            BeatView48.onValueChanged.AsObservable()
            .Subscribe(b =>{
                if(!b)   ChangeBeatType(4);
                else ChangeBeatType(8);
                _selectBeatType.OnNext(Unit.Default);
            })
            .AddTo(this);
        }

        void ChangeBeatType(int a){//引数は3か4
            music.myBeat=a;
            music.myBar=a*a;

            _selectBeatType.OnNext(Unit.Default);
            Debug.Log("せんたく");
        }
    }
}