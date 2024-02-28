using System;
using Code.UI;

namespace Cloudy
{
    public class EndGameObserver : IDisposable
    {
        private readonly EndGamePresenter _endGamePresenter;
        private readonly TimerView _timer;
        private readonly CloudZoneDetectorController _detectorController;
        
        public EndGameObserver(EndGamePresenter endGamePresenter, TimerView timer, CloudZoneDetectorController detectorController)
        {
            _endGamePresenter = endGamePresenter;
            _timer = timer;
            _detectorController = detectorController;
            
            _timer.TimeOver += _endGamePresenter.Show;
            _detectorController.AllZonesCaptured += _endGamePresenter.Show;
        }
        
        public void Dispose()
        {
            _timer.TimeOver -= _endGamePresenter.Show;
            _detectorController.AllZonesCaptured -= _endGamePresenter.Show;
        }

    }
}