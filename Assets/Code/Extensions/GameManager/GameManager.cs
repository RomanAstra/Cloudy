using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Utils
{
    public sealed class GameManager : MonoBehaviour
    {
        private readonly List<IInitializer> _initializers = new();
        private readonly List<IUpdate> _updates = new();
        private readonly List<IGameStart> _starts = new();
        private readonly List<IGameFinish> _finishes = new();
        private readonly List<IGamePause> _pauses = new();
        private readonly List<IGameResume> _resumes = new();
        private readonly List<IDrawGizmos> _drawGizmos = new();
        private GameState _state;
        
        public GameState State => _state;

        [Inject]
        private void Construct(List<IController> controllers)
        {
            for (var index = 0; index < controllers.Count; index++)
            {
                var controller = controllers[index];
                
                Add(controller);
            }
        }

        public void Add(IController controller)
        {
            if (controller is IInitializer initializer)
            {
                _initializers.Add(initializer);
            }

            if (controller is IUpdate update)
            {
                _updates.Add(update);
            }
                
            if (controller is IGameStart start)
            {
                _starts.Add(start);
            }
                
            if (controller is IGameFinish finish)
            {
                _finishes.Add(finish);
            }
                
            if (controller is IGamePause pause)
            {
                _pauses.Add(pause);
            }
                
            if (controller is IGameResume resume)
            {
                _resumes.Add(resume);
            }
                
            if (controller is IDrawGizmos drawGizmos)
            {
                _drawGizmos.Add(drawGizmos);
            }
        }
        public void Remove(IController controller)
        {
            if (controller is IInitializer initializer)
            {
                _initializers.Remove(initializer);
            }

            if (controller is IUpdate update)
            {
                _updates.Remove(update);
            }
                
            if (controller is IGameStart start)
            {
                _starts.Remove(start);
            }
                
            if (controller is IGameFinish finish)
            {
                _finishes.Remove(finish);
            }
                
            if (controller is IGamePause pause)
            {
                _pauses.Remove(pause);
            }
                
            if (controller is IGameResume resume)
            {
                _resumes.Remove(resume);
            }
                
            if (controller is IDrawGizmos drawGizmos)
            {
                _drawGizmos.Remove(drawGizmos);
            }
        }
        public void SetState(GameState state)
        {
            if (_state == state)
                return;

            switch (state)
            {
                case GameState.PLAYING:
                    if (_state == GameState.PAUSED)
                    {
                        for (var i = 0; i < _resumes.Count; i++)
                        {
                            _resumes[i].OnResume();
                        }
                    }
                    else
                    {
                        for (var i = 0; i < _starts.Count; i++)
                        {
                            _starts[i].OnStart();
                        }
                    }
                    break;
                case GameState.PAUSED:
                    for (var i = 0; i < _pauses.Count; i++)
                    {
                        _pauses[i].OnPause();
                    }
                    break;
                case GameState.FINISHED:
                    for (var i = 0; i < _finishes.Count; i++)
                    {
                        _finishes[i].OnFinish();
                    }
                    break;
            }
            
            _state = state;
        }
        
        private void Start()
        {
            for (var index = 0; index < _initializers.Count; index++)
            {
                IInitializer initializer = _initializers[index];
                initializer.OnInitialize();
            }
        }
        private void Update()
        {
            if(_state != GameState.PLAYING)
                return;
            
            float deltaTime = Time.deltaTime;

            var updates = new List<IUpdate>(_updates);
            for (var index = 0; index < updates.Count; index++)
            {
                IUpdate update = updates[index];
                update.OnUpdate(deltaTime);
            }
        }
        private void OnDrawGizmos()
        {
            for (var index = 0; index < _drawGizmos.Count; index++)
            {
                IDrawGizmos drawGizmos = _drawGizmos[index];
                drawGizmos.OnDrawGizmos();
            }
        }
    }
}
