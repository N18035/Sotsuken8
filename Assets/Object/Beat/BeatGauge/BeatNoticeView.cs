using UnityEngine;
using UnityEngine.UI;

namespace Ken.DanceView{
    public class BeatNoticeView : MonoBehaviour
    {
        public Image[] _beatNoticeImage = new Image[8];
        public Image[] _beatNoticeImage2 = new Image[8];

        Color _onColorDown = new  Color32 (255, 255, 255, 255);
        Color _onColorUp = new  Color32 (180, 180, 180, 255);
        Color _offColor = new Color32 (100 ,100 ,100, 150);

        public void DeleteNotice(){
            for(int i=0;i<8;i++){
                _beatNoticeImage[i].color =_offColor;
                _beatNoticeImage2[i].color =_offColor;
            }
        }

        public void PlayBeatNotice(int halfUnit){
            if(Music.Just.Beat == 0){
                _beatNoticeImage[0].color = Music.GetUnit == 0 ? _onColorDown:_offColor;
                _beatNoticeImage[1].color = Music.GetUnit == halfUnit ? _onColorUp:_offColor;
            }else if(Music.Just.Beat == 1){
                _beatNoticeImage[2].color = Music.GetUnit == 0 ? _onColorDown:_offColor;
                _beatNoticeImage[3].color = Music.GetUnit == halfUnit ? _onColorUp:_offColor;
            }else if(Music.Just.Beat == 2){
                _beatNoticeImage[4].color = Music.GetUnit == 0 ? _onColorDown:_offColor;
                _beatNoticeImage[5].color = Music.GetUnit == halfUnit ? _onColorUp:_offColor;
            }else if(Music.Just.Beat == 3){
                _beatNoticeImage[6].color = Music.GetUnit == 0 ? _onColorDown:_offColor;
                _beatNoticeImage[7].color = Music.GetUnit == halfUnit ? _onColorUp:_offColor;
            }
        }

        public void PlayBeatNotice2(int halfUnit){
            if(Music.Just.Beat == 0){
                _beatNoticeImage[0].color = Music.GetUnit == 0 ? _onColorDown:_offColor;
                _beatNoticeImage[1].color = Music.GetUnit ==halfUnit ? _onColorUp:_offColor;
            }else if(Music.Just.Beat == 1){
                _beatNoticeImage[2].color = Music.GetUnit == 0 ? _onColorDown:_offColor;
                _beatNoticeImage[3].color = Music.GetUnit == halfUnit ? _onColorUp:_offColor;
            }else if(Music.Just.Beat == 2){
                _beatNoticeImage[4].color = Music.GetUnit == 0 ? _onColorDown:_offColor;
                _beatNoticeImage[5].color = Music.GetUnit == halfUnit ? _onColorUp:_offColor;
            }else if(Music.Just.Beat == 3){
                _beatNoticeImage[6].color = Music.GetUnit == 0 ? _onColorDown:_offColor;
                _beatNoticeImage[7].color = Music.GetUnit == halfUnit ? _onColorUp:_offColor;
            }else
            if(Music.Just.Beat == 4){
                _beatNoticeImage2[0].color = Music.GetUnit == 0 ? _onColorDown:_offColor;
                _beatNoticeImage2[1].color = Music.GetUnit == halfUnit ? _onColorUp:_offColor;
            }else if(Music.Just.Beat == 5){
                _beatNoticeImage2[2].color = Music.GetUnit == 0 ? _onColorDown:_offColor;
                _beatNoticeImage2[3].color = Music.GetUnit == halfUnit ? _onColorUp:_offColor;
            }else if(Music.Just.Beat == 6){
                _beatNoticeImage2[4].color = Music.GetUnit == 0 ? _onColorDown:_offColor;
                _beatNoticeImage2[5].color = Music.GetUnit == halfUnit ? _onColorUp:_offColor;
            }else if(Music.Just.Beat == 7){
                _beatNoticeImage2[6].color = Music.GetUnit == 0 ? _onColorDown:_offColor;
                _beatNoticeImage2[7].color = Music.GetUnit == halfUnit ? _onColorUp:_offColor;
            }
        }
    }
}

