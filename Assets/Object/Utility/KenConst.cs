namespace Ken
{
    /// <summary>
    /// インゲームの定数
    /// </summary>
    public static class KenConst
    {
        // (+α)
        // レベルデザインに関係がある項目はSerializedObjectとかで外出しする実装にすると、
        // ステージ増やしやすくなったり、設定を弄りやすくなったりします。
        //privateの時はconst
        //他クラスから参照する時はstatic readonly

        /// <summary>
        /// 画面縦幅（画面サイズは縦固定）
        /// </summary>
        public const float WindowHeight = 1080f;


        //Contentの長さの初期値
        //単位はUIのRect座標で、微調整する必要があったりする
        public static readonly float _originStart=-382.96f;
        public static readonly float _originalEnd=388.62f;


        //Zoomの最大拡大値
        public static readonly int MaxZoomLevel = 10;
    }
}
