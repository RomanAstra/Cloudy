using Cloudy.Configs;
using DG.Tweening;
using Utils;

namespace Cloudy.Adapter
{
    public sealed class MovingCloudAdapter : CloudAdapter, 
        IGamePause, IGameResume, IGameFinish
    {
        private readonly MovingCloudHierarchy _hierarchy;
        private readonly MovingCloudConfig _config;
        private Tweener _tween;
        private int _pointIndex = 1;
        private bool _isMoveNextPoint = true;
        
        public MovingCloudAdapter(MovingCloudHierarchy hierarchy, MovingCloudConfig config) : base(hierarchy, config)
        {
            _hierarchy = hierarchy;
            _config = config;
            StartTween();
        }

        void IGamePause.OnPause()
        {
            _tween?.Pause();
        }
        void IGameResume.OnResume()
        {
            _tween?.Play();
        }
        void IGameFinish.OnFinish()
        {
            _tween?.Pause();
        }

        protected override void OnDealDamage(int damage)
        {
            base.OnDealDamage(damage);
            
            if (_currentHitPoints == 0)
                _tween?.Kill();
        }
        private void StartTween()
        {
            _tween = _hierarchy.BodyTransform.DOLocalMove(_hierarchy.TargetPoints[_pointIndex], _config.Duration).SetEase(Ease.Linear).
                SetLink(_hierarchy.BodyTransform.gameObject).OnComplete(() =>
                {
                    if (_isMoveNextPoint)
                    {
                        _pointIndex++;
                        _isMoveNextPoint = _pointIndex < _hierarchy.TargetPoints.Length -1;
                    }
                    else
                    {
                        _pointIndex--;
                        _isMoveNextPoint = _pointIndex == 0;
                    }
                    StartTween();
                });
        }
    }
}