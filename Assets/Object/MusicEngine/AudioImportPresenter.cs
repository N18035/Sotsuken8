using System.Collections;
using UnityEngine;
using System.Windows.Forms; //OpenFileDialog用に使う
using UniRx;
using System;

namespace Ken{
    [RequireComponent(typeof(AudioImporter))]
    [RequireComponent(typeof(AudioSource))]
    public class AudioImportPresenter : Singleton<AudioImportPresenter>
    {
        AudioImporter importer;
        AudioSource audioSource;
        [SerializeField] AudioImportView view;
        
        public ReactiveProperty<String> ClipName => _clipName;
        private readonly ReactiveProperty<string> _clipName = new ReactiveProperty<string>("楽曲名");

        public IObservable<Unit> OnSelectMusic => _selectMusic;
        private Subject<Unit> _selectMusic = new Subject<Unit>();

        string path="";

        private void Start() {
            importer = this.gameObject.GetComponent<AudioImporter>();
            audioSource = this.gameObject.GetComponent<AudioSource>();

            _clipName.Subscribe(_ => view.SetClipName(_clipName.Value))
            .AddTo(this);
        }

        public void MusicSelect()
        {
            OpenFileDialog open_file_dialog = new OpenFileDialog();

            //InputFieldの初期値を代入しておく(こうするとダイアログがその場所から開く)
            open_file_dialog.FileName = path;

            //mp3ファイルを開くことを指定する
            open_file_dialog.Filter = "Mp3ファイル|*.mp3";

            //ファイルが実在しない場合は警告を出す(true)、警告を出さない(false)
            open_file_dialog.CheckFileExists = false;

            //ダイアログを開く
            if (open_file_dialog.ShowDialog() == DialogResult.OK)
            {
                path = open_file_dialog.FileName;

                //FIXME正確にはclipが見つからないかどうかで判定したい
                if(path == "") return;

                Destroy(audioSource.clip);

                StartCoroutine(Import(path));
            }
        }

        IEnumerator Import(string path)
        {
            importer.Import(path);

            while (!importer.isInitialized && !importer.isError)
                yield return null;

            if (importer.isError)
                Debug.LogError(importer.error);

            audioSource.clip = importer.audioClip;

            //完了通知
            _selectMusic.OnNext(Unit.Default);
            //クリップ名取得
            _clipName.Value = importer.audioClip.ToString().Replace("(UnityEngine.AudioClip)","");

            //FIXME最初シーク出来ない問題の暫定対応
            audioSource.Play();
            audioSource.Pause();
        }
    }
}
