using UnityEngine;
using UniRx;

namespace Ken{

    public class ZoomModel : MonoBehaviour
    {
        public IReadOnlyReactiveProperty<int> ZoomLevel => _zoomLevel;
        private readonly ReactiveProperty<int> _zoomLevel = new ReactiveProperty<int>(1);

        public void AddZoomLevel(){
            if(_zoomLevel.Value >= KenConst.MaxZoomLevel) return;
            _zoomLevel.Value ++;
        }

        public void SubZoomLevel(){
            if(_zoomLevel.Value == 1) return;
            _zoomLevel.Value --;
        }

    }
}