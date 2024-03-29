using System;
using System.Collections.Generic;

namespace Ui
{
    public sealed class ViewManager : IViewManager
    {
        private readonly Dictionary<Type, IView> _viewMap = new (16);
        private readonly Stack<IView> _viewsStack = new ();
        private readonly List<IView> _outOfStackWindows = new ();
        private readonly Queue<Action> _modalWindowsCallsQueue = new ();
        private IView _modalWindow;
        
        public void Register<T>(IView<T> view) where T : class, IViewModel
        {
            if (view == null)
            {
                throw new ArgumentNullException();
            }

            Type type = typeof(T);
            if (!_viewMap.ContainsKey(type))
            {
                _viewMap.Add(type, view);
            }
            else
            {
                throw new ArgumentException($"There is another view of type {type}");
            }
        }

        public IView<T> ShowWindow<T>(T viewModel) where T : class, IViewModel
        {
            var view = GetView<T>();

            TryCloseViewIfItAlreadyShown(view);

            viewModel.OnClosed += Close;
            view.Initialize(viewModel);

            if (_viewsStack.Count > 0)
            {
                _viewsStack.Peek().Unfocus();
            }

            view.Show();
            view.Focus();

            _viewsStack.Push(view);
            return view;
        }

        public IView<T> ShowWindowOutOfStack<T>(T viewModel) where T : class, IViewModel
        {
            var view = GetView<T>();

            TryCloseViewIfItAlreadyShown(view);

            viewModel.OnClosed += Close;
            view.Initialize(viewModel);

            view.Show();
            view.Focus();

            _outOfStackWindows.Add(view);
            return view;
        }

        public void ShowModalWindow<T>(T viewModel) where T : class, IViewModel
        {
            if (_modalWindow != null)
            {
                _modalWindowsCallsQueue.Enqueue(() => ShowModalWindow(viewModel));
                return;
            }

            _modalWindow = ShowWindow(viewModel);
        }

        private IView<T> GetView<T>() where T : class, IViewModel
        {
            Type type = typeof(T);
            if (!_viewMap.TryGetValue(type, out IView view))
            {
                throw new ArgumentNullException($"View type of {type} not founded");
            }
            
            return view as IView<T>;
        }

        private void Close(IViewModel viewModel)
        {
            viewModel.OnClosed -= Close;
            if (_viewsStack.Count == 0)
            {
                throw new InvalidOperationException("Try to close not opened view model");
            }

            IView currentView;
            int index = -1;
            for (int i = 0; i < _outOfStackWindows.Count; i++)
            {
                if (_outOfStackWindows[i].ViewModel == viewModel)
                {
                    index = i;
                }
            }

            if (index >= 0)
            {
                currentView = _outOfStackWindows[index];
                _outOfStackWindows.RemoveAt(index);
            }
            else
            {
                while (_viewsStack.Peek().ViewModel != viewModel)
                {
                    IView view = _viewsStack.Peek();
                    view.ViewModel.Close();
                }

                currentView = _viewsStack.Pop();
            }

            currentView.Unfocus();
            currentView.Hide();

            if (_viewsStack.Count > 0)
            {
                IView view = _viewsStack.Peek();
                view.Focus();
            }

            if (_modalWindow != null && _modalWindow.ViewModel == viewModel)
            {
                _modalWindow = null;
                if (_modalWindowsCallsQueue.Count > 0)
                {
                    _modalWindowsCallsQueue.Dequeue().Invoke();
                }
            }
        }

        private void TryCloseViewIfItAlreadyShown(IView view)
        {
            if (_viewsStack.Contains(view) || _outOfStackWindows.Contains(view))
            {
                view.ViewModel.Close();
            }
        }
    }
}
