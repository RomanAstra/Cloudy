using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Utils
{
    public sealed class GameManager : MonoBehaviour
    {
        private readonly List<IInitializer> _initializers = new();
        private readonly List<IUpdate> _updates = new();
        private readonly List<IDrawGizmos> _drawGizmos = new();

        [Inject]
        private void Construct(List<IController> controllers)
        {
            for (var index = 0; index < controllers.Count; index++)
            {
                var controller = controllers[index];
                if (controller is IInitializer initializer)
                {
                    _initializers.Add(initializer);
                }

                if (controller is IUpdate update)
                {
                    _updates.Add(update);
                }
                
                if (controller is IDrawGizmos drawGizmos)
                {
                    _drawGizmos.Add(drawGizmos);
                }
            }
        }

        private void Start()
        {
            for (var index = 0; index < _initializers.Count; index++)
            {
                IInitializer initializer = _initializers[index];
                initializer.OnStart();
            }
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;
            for (var index = 0; index < _updates.Count; index++)
            {
                IUpdate update = _updates[index];
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
