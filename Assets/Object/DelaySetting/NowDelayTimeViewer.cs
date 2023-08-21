using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Ken
{
    public class NowDelayTimeViewer : Singleton<NowDelayTimeViewer>
    {
        [SerializeField] Text startTime;
        [SerializeField] InputField delayText;

        public void ChangeTime(float time){
            // startTime.text= time.ToString("F3");
            delayText.text = time.ToString("F3");
        }

    }
}