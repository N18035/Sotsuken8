using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ken.Main{
    [RequireComponent(typeof(Image))]
    public class SoundWave : MonoBehaviour
    {
        //音声clipにのみ依存して動く。それ以外は

        [SerializeField]private int width = 15000;
        [SerializeField]private int height = 240;
        [SerializeField]private Color waveformColor = Color.green;
        [SerializeField]private float sat = .5f;//謎の数字。多分彩度か入力信号を上限と下限の飽和値に制限する

        Image img;
        AudioCheck check;

        void Start() {
            check = AudioCheck.I;
            img = this.gameObject.GetComponent<Image>();    
        }

        //疑似スタートで動作
        public void CreateSoundWave()
        {
            if(check.TryGetClip(out var clip)){
                Texture2D texture = PaintWaveformSpectrum(clip, sat, width, height, waveformColor);
                img.sprite = null;
                img.overrideSprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            }
        }

        private Texture2D PaintWaveformSpectrum(AudioClip audio, float saturation, int width, int height, Color col) {
            Texture2D tex = new Texture2D(width, height, TextureFormat.RGBA32, false);

            //全データ分の配列を用意
            float[] samples = new float[audio.samples * audio.channels];

            //試しにwidthを同じにしてみる//失敗
            // width = samples.Length;

            //最終的に描画する配列
            float[] waveform = new float[width];

            //GetDataで波形を入手
            audio.GetData(samples, 0);

            //若干波形が細かくなる
            float packSize = ((float)samples.Length / (float)width);
            int s = 0;

            //飽和処理？
            for (float i = 0; Mathf.RoundToInt(i) < samples.Length && s < waveform.Length; i += packSize){
                //int型に変換
                waveform[s] = Mathf.Abs(samples[Mathf.RoundToInt(i)]);
                s++;
            }

            //背景を黒く塗りつぶす
            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    tex.SetPixel(x, y, Color.black);
                }
            }

            //画像の横幅:減らすと単純に描画しない    
            for (int x = 0; x < waveform.Length; x++) {
                //waveformの値回、"点"を置いて線にしてる
                //fは大きくすると線が伸びる
                for(int y = 0; y <= waveform[x] * ((float)height *.8f); y++) {
                    //上下に同じ周波数を描画していく。あくまで座標を参照している
                    int xc = x;
                    tex.SetPixel(xc, ( height / 2 ) + y, col);
                    tex.SetPixel(xc, ( height / 2 ) - y, col);
                }
            }

            tex.Apply();
    
            return tex;
        }
}
}
