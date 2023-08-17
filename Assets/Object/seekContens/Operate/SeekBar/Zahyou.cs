using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

namespace Ken.Main.SeekBar
{
    [RequireComponent(typeof(Button))]
    public class Zahyou : MonoBehaviour
    {
        Button button;
        //外部
        [SerializeField] RectTransform canvasRect;
        [SerializeField] Content _contentZoom;
        [SerializeField] AudioControl _audioController;
        [SerializeField] Music _music;
        [SerializeField] AudioSource _audio;

        private void Start() {
            button = this.gameObject.GetComponent<Button>();

            //停止、動作時クリック変換
            button.onClick.AsObservable()
            .Where(_ => _audio.clip != null)
            .Subscribe(_ =>{ 
                //マウスクリックはスクリーン座標
                CalcNowMusicTime(Input.mousePosition ,out var now);
                _audioController.Seek(now);
                //音楽を変更
                _music.LoadTiming();
            })
            .AddTo(this);
        }


        void CalcNowMusicTime(Vector3 mousePos, out float now){
            //引数はスクリーン座標

            //キャンバスと画面サイズの倍率を取る
            var magnification = canvasRect.sizeDelta.x / Screen.width;

            //スクリーン座標は画面左はしが0,0でCanvasは中心が0,0なのでこの差を解消する
            // 倍率をかけてキャンバス座標にして、起点を揃えた部分がこれ。
            mousePos.x = mousePos.x * magnification - canvasRect.sizeDelta.x / 2;
            mousePos.y = mousePos.y * magnification - canvasRect.sizeDelta.y / 2;
            mousePos.z = transform.localPosition.z;

            //(マウスの場所)/(座標検知全体)) = 楽曲のパーセント
            var nowp = (mousePos.x - _contentZoom.NowStart) / (_contentZoom.NowEnd - _contentZoom.NowStart);
            now = nowp * _audio.clip.length;
        }
    }
}
