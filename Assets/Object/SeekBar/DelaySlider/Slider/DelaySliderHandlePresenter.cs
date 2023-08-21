using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Ken
{
public class DelaySliderHandlePresenter : MonoBehaviour
{
    bool isGrag;
    DelaySliderManager manager;
    [SerializeField] DelaySliderHandleView view;
    [SerializeField] DelaySliderPresenter presenter;
    [SerializeField] ObservableEventTrigger eventTrigger;
    
    void Start()
    {
        manager = DelaySliderManager.I;

        //ハンドルにふれる系
        eventTrigger.OnPointerEnterAsObservable()
            .Subscribe(_ =>{
                manager.ChangeNow(presenter.ID);
                Selected();
            })
            .AddTo(this);

        eventTrigger.OnPointerDownAsObservable()
            .Subscribe(_ =>{
                isGrag=true;
                manager.ChangeNow(presenter.ID);
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
        manager.OnNow
        .Where(now => now == presenter.ID)
        .Subscribe(_ => view.SetColor(true))
        .AddTo(this);

        manager.OnNow
        .Where(now => now != presenter.ID)
        .Subscribe(_ => view.SetColor(false))
        .AddTo(this);
    }

    void Selected(){
        view.BigImage();
        view.SetColor(true);
    }
}

}
