using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

namespace Ken.Delay{
    public class TimePresenter : MonoBehaviour
    {
        Text startTime;
        [SerializeField] DelaySliderManager manager;

        private void Start() {
            startTime = this.gameObject.GetComponent<Text>();

            manager.OnNowChanged
            .Subscribe(_ => ChangeTime())
            .AddTo(this);
        }

        public void ChangeTime(){
            startTime.text= manager.GetNowValue().ToString("F3");
        }

    }
}