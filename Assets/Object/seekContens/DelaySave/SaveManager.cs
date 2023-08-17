using System.IO;
using UnityEngine;
using Ken.Delay;
using Sirenix.OdinInspector;//SerializedMonoBehaviourを使うのに必要
using System.Windows.Forms; //OpenFileDialog用に使う
using UniRx;
using System;

namespace Ken.Save
{
    public class SaveManager : MonoBehaviour 
    {
        [SerializeField] CountPresenter count;
        [SerializeField] DelaySliderManager manager;

        public IReactiveProperty<string> Info => _inf;
        private readonly ReactiveProperty<string> _inf = new ReactiveProperty<string>();
        public IObservable<Unit> OnLoad => load;
        private readonly Subject<Unit> load = new Subject<Unit>();

        public IReactiveProperty<string> NowPath => _path;
        private readonly ReactiveProperty<string> _path = new ReactiveProperty<string>("");


        //確認用
        [SerializeField]DelayData Cdata;

        public void Save(){
            if(_path.Value.Equals("")) return;
            WriteJson();
            _inf.Value = "上書き保存しました";
            SystemSEManager.I.Good();
        }
        
        // jsonとしてデータを保存
        public void NewSave()
        {
            // ダイアログボックスの表示()
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.FileName = "ファイル";
            sfd.InitialDirectory = "";
            sfd.Filter = "jsonファイル|*.json";
            sfd.FilterIndex = 2;
            sfd.Title = "保存先のファイルを選択してください";
            sfd.RestoreDirectory = true;//ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                //フィールドにパスを保存
                _path.Value = sfd.FileName;
                WriteJson();
                _inf.Value = "新規保存しました";
                SystemSEManager.I.Better();
                MessageBox.Show("新規保存しました"+sfd.FileName, "通知", MessageBoxButtons.OK);
                
            }
        }

        void WriteJson(){
            DelayData data = count.GetDelayData();
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(_path.Value, json);
        }

        // jsonファイル読み込み
        public void Load()
        {
            OpenFileDialog open_file_dialog = new OpenFileDialog();

            //InputFieldの初期値を代入しておく(こうするとダイアログがその場所から開く)
            open_file_dialog.FileName = "";
            //jsonファイルを開くことを指定する
            open_file_dialog.Filter = "jsonファイル|*.json";
            //ファイルが実在しない場合は警告を出す(true)、警告を出さない(false)
            open_file_dialog.CheckFileExists = true;


            //ダイアログを開いてpathを取得
            open_file_dialog.ShowDialog();
            string path="";
            path = open_file_dialog.FileName;
            if(path.Equals("")) return;

            //フィールドに保存
            _path.Value = path;
            //ここから読み込み
            StreamReader rd = new StreamReader(path);

            string json = rd.ReadToEnd();                           // ファイル内容全て読み込む
            rd.Close();                                             // ファイル閉じる
                                                                    
            var data = JsonUtility.FromJson<DelayData>(json);            // jsonファイルを型に戻して返す
            Cdata = data;

            var loadComplete =  manager.JsonToDelayTimeData(data);
            if(loadComplete)   _inf.Value = "ロード完了";
            else    _inf.Value = "一部開始点が読み込めませんでした。ロードしたデータに間違いはありませんか？";

            load.OnNext(Unit.Default);
        }
    }
}

