using UnityEngine;
using UnityEngine.UI;
using System;

namespace Ken
{
    public class TimeLineView : MonoBehaviour
    {
        [SerializeField] ZoomModel _zoomController;
        [SerializeField] GameObject TLImage;

        //メモリのもとになるプレファブ
        [SerializeField] GameObject num;

        GameObject[] Memory;


        /// <summary>
        /// 初期設定
        /// </summary>
        public void ReadyTimeLineView(TLData data){
            //子要素を破壊
            foreach(Transform child in TLImage.transform){
                Destroy(child.gameObject);
            }
            Array.Resize(ref Memory,data.SecondCount);

            //目盛りの生成(初回しかやらない)
            for(int j=0;j<data.ScalePositionsSecond.Length;j++){
                Memory[j] = Instantiate(num, new Vector3(data.ScalePositionsSecond[j], 0, 0), Quaternion.identity);
                Memory[j].transform.SetParent(TLImage.transform,false);
            }

            EditSeconds();
            SelectShowSeconds();
        }

        /// <summary>
        /// 位置変更と表示詳細度の変更
        /// </summary>
        public void ResizeTimeLineView(TLData data){
            for(int i=0;i<Memory.Length;i++){
                Memory[i].GetComponent<RectTransform>().localPosition = new Vector3(data.ScalePositionsSecond[i], 0,0);
            }
            SelectShowSeconds();
        }

        /// <summary>
        /// 目盛りのtextを編集
        /// </summary>
        void EditSeconds(){
            for(int i=0;i<Memory.Length;i++){
                Memory[i].GetComponent<Text>().text = i.ToString();
            }
        }

        /// <summary>
        /// 目盛りの表示する、しないを変更
        /// </summary>
        void SelectShowSeconds(){
            //初期化
            for(int i=0;i<Memory.Length;i++){
                Memory[i].SetActive(true);
            }

            //最大値+1-今回のzoomレベル=どれくらい削るか
            int delete = KenConst.MaxZoomLevel+1 - _zoomController.ZoomLevel.Value;

            //削る
            for(int i=0;i<Memory.Length;i++){
                if(i % delete != 0)  Memory[i].SetActive(false);
            }
        }
    }
}
