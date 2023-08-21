using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

namespace Ken
{
    public class AudioImportView : MonoBehaviour
    {
        [SerializeField] Text text;
        [SerializeField] Button button;

        public IObservable<Unit> OnClick => click;
        private readonly Subject<Unit> click = new Subject<Unit>();

    
        public void Start(){

            button.onClick.AsObservable()
            .Subscribe(_ => click.OnNext(Unit.Default))
            .AddTo(this);
        }

        //クリップはxxxx UnityEngineみたいな感じで送られてくるから、改行で上手く隠す
        public void SetClipName(string name){
            text.text=name;
        }
    }
}

