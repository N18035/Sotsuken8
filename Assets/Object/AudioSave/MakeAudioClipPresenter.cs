using System;
using UnityEngine;
using System.Windows.Forms; //OpenFileDialog用に使う
using System.IO;
using Ken.Delay;

public class MakeAudioClipPresenter : MonoBehaviour
{
    [SerializeField] AudioSource mainAudio;
    [SerializeField] AudioSource beat;
    [SerializeField] DelaySliderManager manager;
    [SerializeField] MakeAudioClipModel model;

    public void Make()
    {
        Debug.Log(model);
        if(mainAudio==null || beat.clip == null)   return;

        DelayData Ddata = manager.CreateDelayTimeData();
        float[] combinedClipData = model.CombineFromData(Ddata,mainAudio.clip,beat.clip);

        var clip = model.CombineClip(combinedClipData);

        // //生成したのをファイルとして保存
        Save(clip);
    }
    void Save(AudioClip clip){
        // ダイアログボックスの表示()
        SaveFileDialog sfd = new SaveFileDialog();

        sfd.FileName = "ファイル";
        sfd.InitialDirectory = "";
        sfd.Filter = "wavファイル|*.wav";
        sfd.FilterIndex = 2;
        sfd.Title = "保存先のファイルを選択してください";
        sfd.RestoreDirectory = true;//ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする

        if (sfd.ShowDialog() == DialogResult.OK)
        {
            File.WriteAllBytes(sfd.FileName, WavUtility.FromAudioClip(clip));
            SystemSEPresenter.I.Better();
            MessageBox.Show("新規保存しました"+sfd.FileName, "通知", MessageBoxButtons.OK);
        }
    }

}
