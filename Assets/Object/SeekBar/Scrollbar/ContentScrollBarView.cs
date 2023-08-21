using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

namespace Ken.Main{
    public class ContentScrollBarView : MonoBehaviour
    {
        [SerializeField] ContentLengthPresenter content;
        [SerializeField] Scrollbar scrollbar;
        void Start()
        {
            scrollbar.OnValueChangedAsObservable()
            .Subscribe(v => content.Moved(v))
            .AddTo(this);
        }

    }
}

