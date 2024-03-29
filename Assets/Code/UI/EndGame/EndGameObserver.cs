using System;
using Code.UI;

namespace Cloudy
{
    public class EndGameObserver : IDisposable
    {
        private readonly EndGamePresenter _endGamePresenter;
        private readonly TimerViewModel _timer;
        private readonly CloudZoneDetectorController _detectorController;
        
        public EndGameObserver(EndGamePresenter endGamePresenter, TimerViewModel timer, CloudZoneDetectorController detectorController)
        {
            _endGamePresenter = endGamePresenter;
            _timer = timer;
            _detectorController = detectorController;
            
            _timer.OnTimeOver += _endGamePresenter.Show;
            _detectorController.AllZonesCaptured += _endGamePresenter.Show;
        }
        
        public void Dispose()
        {
            _timer.OnTimeOver -= _endGamePresenter.Show;
            _detectorController.AllZonesCaptured -= _endGamePresenter.Show;
        }
    }
}