using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extensions
{
    public sealed class ScreenManager : MonoBehaviour
    {
        private static ScreenManager _instance;

        public static event Func<BaseScreen, float> ScreenHide;
        public static event Func<BaseScreen, float> ScreenShow;

        private void Awake()
        {
            _instance = this;

            var menus = GetComponentsInChildren<BaseScreen>(true);

            foreach (var menu in menus)
            {
                _menus.Add(menu.GetType(), menu);
                menu.gameObject.SetActive(false);
            }
        }

        public static BaseScreen Last { get; private set; }

        public static void Show<T>() where T : BaseScreen
        {
            Show<T>(null);
        }
        public static void Show<T>(object data) where T : BaseScreen
        {
            if (_instance == null)
            {
                Debug.LogWarning(nameof(ScreenManager) + ": there is no instance");
                return;
            }

            if (_instance._isShowing)
            {
                Debug.LogWarning(nameof(ScreenManager) + ": is still showing screen");
                return;
            }

            if (!_instance._menus.ContainsKey(typeof(T)))
            {
                Debug.LogWarning(nameof(ScreenManager) + ": does not contain screen " + typeof(T).Name);
                return;
            }

            _instance.StartCoroutine(_instance.DoShow(_instance._menus[typeof(T)], data));
        }
        public static void Show(BaseScreen screen, object data = null)
        {
            if (_instance == null)
            {
                Debug.LogWarning(nameof(ScreenManager) + ": there is no instance");
                return;
            }

            if (_instance._isShowing)
            {
                Debug.LogWarning(nameof(ScreenManager) + ": is still showing screen");
                return;
            }

            if (!_instance._menus.ContainsValue(screen))
            {
                Debug.LogWarning(nameof(ScreenManager) + ": does not contain screen " + screen.GetType().Name);
                return;
            }

            _instance.StartCoroutine(_instance.DoShow(screen, data));
        }
        public static bool IsCurrent<T>() where T : BaseScreen
        {
            if (_instance == null)
                return false;

            if (_instance._current == null)
                return false;

            return _instance._current.GetType() == typeof(T);
        }
        public static T Get<T>() where T : BaseScreen
        {
            if (_instance == null)
            {
                Debug.LogWarning(nameof(ScreenManager) + ": there is no instance");
                return null;
            }

            if (!_instance._menus.ContainsKey(typeof(T)))
            {
                Debug.LogWarning(nameof(ScreenManager) + ": does not contain screen " + typeof(T).Name);
                return null;
            }

            return (T)_instance._menus[typeof(T)];
        }

#if UNITY_EDITOR
        [UnityEditor.MenuItem("GameObject/Screen Manager", false, 11)]
        private static void Create()
        {
            var manager = FindObjectOfType<ScreenManager>();
            if (manager != null)
            {
                Debug.LogWarning(nameof(ScreenManager) + ": is already exists");
                return;
            }
        
            new GameObject(nameof(ScreenManager), typeof(ScreenManager)).transform.SetParent(UnityEditor.Selection.activeTransform, false);
        }
#endif

        private IEnumerator DoShow(BaseScreen menu, object data)
        {
            if (menu == null || menu == _current)
                yield break;

            _isShowing = true;

            Last = _current;
            _current = menu;

            var time = 0f;

            if (Last != null)
            {
                if (ScreenHide != null)
                    time = ScreenHide(Last);
                if(time > 0)
                    yield return new WaitForSecondsRealtime(time);

                time = Last.Hide();
                if (time > 0)
                    yield return new WaitForSecondsRealtime(time);
            }

            time = _current.Show(data);
            if(time > 0)
                yield return new WaitForSecondsRealtime(time);

            if (ScreenShow != null)
                time = ScreenShow(_current);
            if(time > 0)
                yield return new WaitForSecondsRealtime(time);

            _isShowing = false;
        }
    
        private readonly Dictionary<Type, BaseScreen> _menus = new Dictionary<Type, BaseScreen>();
        private BaseScreen _current;
        private bool _isShowing;
    }
}