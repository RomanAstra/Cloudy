using Extensions;

namespace Cloudy.UI.Screens
{
    public sealed class MainScreen : BaseScreen
    {
        protected override void Awake()
        {
            base.Awake();

            App.CurrentLevel = 1;
        }
    }
}