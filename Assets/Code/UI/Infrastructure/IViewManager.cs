namespace Ui
{
    public interface IViewManager
    {
        void Register<T>(IView<T> view) where T : class, IViewModel;
        IView<T> ShowWindow<T>(T viewModel) where T : class, IViewModel;
        void ShowModalWindow<T>(T viewModel) where T : class, IViewModel;
        IView<T> ShowWindowOutOfStack<T>(T viewModel) where T : class, IViewModel;
    }
}