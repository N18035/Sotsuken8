using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Ken.Main;

namespace Ken
{
    public class ZoomControllerPresenter : MonoBehaviour
    {
        [SerializeField] ZoomModel model;
        [SerializeField] Text text;
        [SerializeField] Button Plus;
        [SerializeField] Button Minus;
        AudioCheckPresenter check;


        void Start(){
            check = AudioCheckPresenter.I;
            
            model.ZoomLevel
            .Subscribe(zl =>text.text=zl.ToString())
            .AddTo(this);

            Plus.onClick.AsObservable()
            .Where(_ => !check.ClipIsNull())
            .Subscribe(_ => model.AddZoomLevel())
            .AddTo(this);

            Minus.onClick.AsObservable()
            .Where(_ => !check.ClipIsNull())
            .Subscribe(_ => model.SubZoomLevel())
            .AddTo(this);
        }
    }
}

