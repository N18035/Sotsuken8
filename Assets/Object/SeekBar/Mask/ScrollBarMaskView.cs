using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Ken;

public class ScrollBarMaskView : MonoBehaviour
{
    [SerializeField] Image ViewPortMask;
    [SerializeField] ZoomModel _zoom;
    void Start()
    {
            _zoom.ZoomLevel
            .Where(l => l==1)
            .Subscribe(_ => ViewPortMask.enabled=true)
            .AddTo(this);

            _zoom.ZoomLevel
            .Where(l => l!=1)
            .Subscribe(_ => ViewPortMask.enabled=false)
            .AddTo(this);
    }
}
