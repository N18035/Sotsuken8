using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;
using System.Collections;

namespace Ken
{
    public class AudioImportView : MonoBehaviour
    {
        [SerializeField] Text text;
        [SerializeField] Button button;
        [SerializeField] Text notion;
        private bool isBlinking = true; // テキストが点滅中かどうかを示すフラグ

        public IObservable<Unit> OnClick => click;
        private readonly Subject<Unit> click = new Subject<Unit>();

    
        public void Start(){

            button.onClick.AsObservable()
            .Subscribe(_ => click.OnNext(Unit.Default))
            .AddTo(this);

            StartCoroutine(BlinkText());
        }

        //クリップはxxxx UnityEngineみたいな感じで送られてくるから、改行で上手く隠す
        public void SetClipName(string name){
            text.text=name;
        }

        // 点滅を停止するメソッド
        public void StopBlinking()
        {
            isBlinking = false; // 点滅を停止
            notion.enabled = true; // テキストを表示
        }

        //点滅
        private IEnumerator BlinkText()
        {
            float blinkInterval = 0.5f;
            while (isBlinking)
            {
                notion.enabled = !notion.enabled;
                yield return new WaitForSeconds(blinkInterval);
            }
        }
    }
}

