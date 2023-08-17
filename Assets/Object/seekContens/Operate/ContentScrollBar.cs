using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

namespace Ken.Main{
    public class ContentScrollBar : MonoBehaviour
    {
        [SerializeField] Content con;
        void Start()
        {
            this.gameObject.GetComponent<Scrollbar>().OnValueChangedAsObservable()
            .Subscribe(v => con.Moved(v))
            .AddTo(this);
        }

    }
}

