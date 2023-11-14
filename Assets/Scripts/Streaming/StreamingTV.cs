using PlayerActions;

namespace VideoStreaming
{
    public class StreamingTV : StreamingObject
    {
        public override bool TryTogglePlayPause()
        {
            if (!_canBePaused) return false;
            if (_isPlaying)
            {
                _isPlaying = false;
                WebGLStreamController.Instance.PauseStream(_streamIndex);
                return true;
            }
            else
            {
                _isPlaying = true;
                WebGLStreamController.Instance.PlayStream(_streamIndex);
                return true;
            }
        }
    }
}