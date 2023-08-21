using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using UnityEngine.EventSystems;
using Ken.Delay;

namespace Ken
{
public class DelaySliderHandlePresenter : MonoBehaviour
{
    bool isGrag;
    [SerializeField] DelaySliderHandleView view;
    [SerializeField] DelaySliderManager Manager;
    [SerializeField] SliderPresenter sliderP;
    [SerializeField] ObservableEventTrigger eventTrigger;
    
    void Start()
    {
        //ハンドルにふれる系
        eventTrigger.OnPointerEnterAsObservable()
            .Subscribe(_ =>{
                Manager.ChangeNow(sliderP.ID);
                Selected();
            })
            .AddTo(this);

        eventTrigger.OnPointerDownAsObservable()
            .Subscribe(_ =>{
                isGrag=true;
                Manager.ChangeNow(sliderP.ID);
                Selected();
            })
            .AddTo(this);

        eventTrigger.OnPointerExitAsObservable()
            .Where(_ => !isGrag)
            .Subscribe(_ => view.SmallImage())
            .AddTo(this);
        
        eventTrigger.OnPointerUpAsObservable()
            .Subscribe(_ =>{
                view.SmallImage();
                isGrag=false;
            })
            .AddTo(this);

        //外部系
        Manager.OnNowChanged
        .Where(now => now == sliderP.ID)
        .Subscribe(_ => view.SetColor(true))
        .AddTo(this);

        Manager.OnNowChanged
        .Where(now => now != sliderP.ID)
        .Subscribe(_ => view.SetColor(false))
        .AddTo(this);
    }

    void Selected(){
        view.BigImage();
        view.SetColor(true);
    }
}

}
