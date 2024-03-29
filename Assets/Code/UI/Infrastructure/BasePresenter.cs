using Ui;
using UnityEngine;

namespace Code.UI
{
    public abstract class BasePresenter<TView, TViewModel> where TView : MonoBehaviour, 
        IView<TViewModel> where TViewModel : class, IViewModel
    {
        protected BasePresenter(IViewManager viewManager)
        {
            var view = Object.FindFirstObjectByType<TView>(FindObjectsInactive.Include);
            viewManager.Register(view);
        }
    }
}