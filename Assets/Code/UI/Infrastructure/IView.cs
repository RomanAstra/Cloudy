namespace Ui
{
    public interface IView
    {
        IViewModel ViewModel { get; }
        void Show();
        void Hide();
        void Unfocus();
        public void Focus();
    }
    
    public interface IView<in T> : IView where T : IViewModel
    {
        void Initialize(T viewModel);
    }
}
