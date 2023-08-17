using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace Ken.Main
{
    public class TimeLineView : MonoBehaviour
    {
        [SerializeField] Ken.Zoom.ZoomModel _zoomController;
        // [SerializeField] TimeLine _timeline;
        [SerializeField] GameObject TLImage;
        [SerializeField] GameObject TLImage2nd;
        [SerializeField] Music _music;
        [SerializeField] AudioSource _audioSource;

        //メモリのもとになるプレファブ
        [SerializeField] GameObject num;

        public GameObject[] Memory;
        public GameObject[] Memory2nd;


        /// <summary>
        /// 初期設定
        /// </summary>
        public void ReadyTimeLineView(Ken.Main.TLData data){
            //子要素を破壊
            foreach(Transform child in TLImage.transform){
                Destroy(child.gameObject);
            }

            //秒も破壊
            foreach(Transform child in TLImage2nd.transform){
                Destroy(child.gameObject);
            }

            Array.Resize(ref Memory,data.MaxDisplayBar);
            Array.Resize(ref Memory2nd,data.SecondCount);

            //目盛りの生成(初回しかやらない)
            for(int j=0;j<data.ScalePositionsBar.Length;j++){
                Memory[j] = Instantiate(num, new Vector3(data.ScalePositionsBar[j], -5f, 0), Quaternion.identity);
                Memory[j].transform.SetParent(TLImage.transform,false);//でふぉ
            }

            for(int j=0;j<data.ScalePositionsSecond.Length;j++){
                Memory2nd[j] = Instantiate(num, new Vector3(data.ScalePositionsSecond[j], 4f, 0), Quaternion.identity);
                Memory2nd[j].transform.SetParent(TLImage2nd.transform,false);//でふぉ
            }

            SetMoji();
            SelectMoji();
        }

        /// <summary>
        /// 位置変更と表示詳細度の変更
        /// </summary>
        public void ResizeTimeLineView(Ken.Main.TLData data){
            for(int i=0;i<Memory.Length;i++){
                Memory[i].GetComponent<RectTransform>().localPosition = new Vector3(data.ScalePositionsBar[i], -5f,0);
            }

            for(int i=0;i<Memory2nd.Length;i++){
                Memory2nd[i].GetComponent<RectTransform>().localPosition = new Vector3(data.ScalePositionsSecond[i], 4f,0);
            }

            SelectMoji();
        }

        /// <summary>
        /// 目盛りのtextを編集
        /// </summary>
        void SetMoji(){
            int n=0;
            for(int i=0;i<Memory.Length;i++){
                //Beatの時は拍数で調節する
                if(i%_music.myBeat==0){
                    Memory[i].GetComponent<Text>().text = n.ToString();
                    n++;
                }
            }

            // string memo;
            for(int i=0;i<Memory2nd.Length;i++){
                // if(i)
                Memory2nd[i].GetComponent<Text>().text = i.ToString();
                // n++;
            }
        }

        /// <summary>
        /// 目盛りの表示する、しないを変更
        /// </summary>
        void SelectMoji(){
            //初期化
            for(int i=0;i<Memory.Length;i++){
                Memory[i].SetActive(true);
            }

            for(int i=0;i<Memory2nd.Length;i++){
                Memory2nd[i].SetActive(true);
            }

            //最大値+1-今回のzoomレベル=どれくらい削るか
            int delete = _music.myBeat * (KenConst.MaxZoomLevel+1 - _zoomController.ZoomLevel.Value);
            // int Length = (int)(_audioSource.clip.length / 60f);

            for(int i=0;i<Memory.Length;i++){
                if(i%_music.myBeat!=0)  Memory[i].SetActive(false);
                if(i %  delete!= 0)  Memory[i].SetActive(false);
            }

            for(int i=0;i<Memory2nd.Length;i++){
                if(i % delete != 0)  Memory2nd[i].SetActive(false);
            }
        }
    }
}
