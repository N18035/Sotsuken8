public struct BeatSoundData{
        public int Number { get; }
        public bool Is1Up { get; }
        public bool IsOnly1 { get; }

        public BeatSoundData(int n, bool isup, bool isonly1) { Number = n; Is1Up = isup; IsOnly1=isonly1; }
    }
