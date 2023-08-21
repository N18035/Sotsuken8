using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

namespace Ken.Delay{
    public class TimePresenter : MonoBehaviour
    {
        [SerializeField] Text startTime;
        DelaySliderManager manager;

        private void Start() {
            manager = DelaySliderManager.I;

            manager.OnNowChanged
            .Subscribe(_ => ChangeTime())
            .AddTo(this);
        }

        public void ChangeTime(){
            startTime.text= manager.GetNowValue().ToString("F3");
        }

    }
}