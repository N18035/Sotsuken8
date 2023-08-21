using UnityEngine;
using UnityEngine.UI;

namespace Ken{
    public class BeatNoticeView : MonoBehaviour
    {
        public Image[] _beatNoticeImage = new Image[8];

        Color _onColorDown = new  Color32 (255, 255, 255, 255);
        Color _offColor = new Color32 (100 ,100 ,100, 150);

        //表示を一旦消す
        public void DeleteNotice(){
            for(int i=0;i<8;i++){
                _beatNoticeImage[i].color =_offColor;
            }
        }

        //追加8ビート表示
        public void Show8Notice(){
            for(int i=4;i<8;i++){
                _beatNoticeImage[i].enabled=true;
            }
        }

        //追加8ビート非表示
        public void Hide8Notice(){
            for(int i=4;i<8;i++){
                _beatNoticeImage[i].enabled=false;
            }
        }

        //ビートの表示
        public void PlayBeatNotice(int n){
            _beatNoticeImage[n].color = Music.GetUnit == 0 ? _onColorDown:_offColor;
        }
    }
}

