namespace Code.UI
{
    public sealed class LevelProgressModel : ILevelProgressModel
    {
        public int Percent { get; }

        public LevelProgressModel(int percent)
        {
            Percent = percent;
        }
    }
}