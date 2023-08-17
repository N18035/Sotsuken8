using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Ken{
    public class AudioLoop : MonoBehaviour
    {
        [SerializeField] InputField startPoint;
        [SerializeField] InputField endPoint;
        [SerializeField] AudioControl _audioControl;
        [SerializeField] Toggle toggle;
        [SerializeField] AudioImport _audioImport;


        void Start(){
            _audioImport.OnSelectMusic
            .Subscribe(_ =>{
                toggle.isOn=false;
            })
            .AddTo(this);

            toggle.onValueChanged.AsObservable()
            .Subscribe(_ => OnLoop())
            .AddTo(this);
            
        }


        //フラグを勝手に向こうが識別します
        public void OnLoop(){
            int s = Int32.Parse(startPoint.text);
            int e = Int32.Parse(endPoint.text);

            _audioControl.Loop(s,e);
        }
    }
}

