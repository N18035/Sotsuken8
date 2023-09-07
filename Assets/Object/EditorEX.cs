#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

//エディタ拡張
//Presenterの実装が終われば消します

namespace Ken
{
    [CustomEditor(typeof(AudioImportPresenter))]
    public class EditorEx : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector(); // 通常のInspector表示

            var presenter = (AudioImportPresenter)target;

            if (GUILayout.Button("セット"))
            {
                presenter.SetMusicOnEditor();
            }
        }
    }
}
#endif