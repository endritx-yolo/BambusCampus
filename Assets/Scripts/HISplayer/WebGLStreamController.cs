using HISPlayerAPI;
using UnityEngine;

namespace VideoStreaming
{
    public class WebGLStreamController : HISPlayerManager
    {
        private static WebGLStreamController _instance;
        public static WebGLStreamController Instance => _instance;

        protected override void Awake()
        {
            if (_instance == null)
                _instance = this;
            else
            {
                //Destroy(gameObject);
                return;
            }

            base.Awake();
            SetUpPlayer();
        }

#if UNITY_EDITOR
        private void OnApplicationQuit() => Release();
#endif

        public void PlayStream(int streamIndex)
        {
            if (streamIndex >= multiStreamProperties.Count)
            {
                Debug.LogError(
                    $"Stream index '{streamIndex}' is greater or equals to the total stream count of {multiStreamProperties.Count}.");
                return;
            }

            Debug.Log($"Playing stream {streamIndex}");
            Play(streamIndex);
        }

        public void PauseStream(int streamIndex)
        {
            if (streamIndex >= multiStreamProperties.Count)
            {
                Debug.LogError(
                    $"Stream index '{streamIndex}' is greater or equals to the total stream count of {multiStreamProperties.Count}.");
                return;
            }

            Debug.Log($"Pausing stream {streamIndex}");
            Pause(streamIndex);
        }

        public void SetNewTrackURL(int streamIndex, string newUrl)
        {
            if (streamIndex >= multiStreamProperties.Count)
            {
                Debug.LogError(
                    $"Stream index '{streamIndex}' is greater or equals to the total stream count of {multiStreamProperties.Count}.");
                return;
            }

            //ChangeVideoContent(streamIndex, newUrl);
        }

        public void StopAllLiveStreams()
        {
            for (int i = 0; i < multiStreamProperties.Count; i++)
                Stop(i);
        }

        public void SetNewVolume(float volume)
        {
            volume = Mathf.Clamp(volume, 0f, 1f);
            for (int i = 0; i < multiStreamProperties.Count; i++)
                SetVolume(i, volume);
        }

        public void ChangeSpeedRate(float newSpeed)
        {
            for (int i = 0; i < multiStreamProperties.Count; i++)
                SetPlaybackSpeedRate(i, newSpeed);
        }

        public void ToggleSubtitles(int streamIndex, int ccTrackIndex)
        {
            if (streamIndex >= multiStreamProperties.Count)
            {
                Debug.LogError(
                    $"Stream index '{streamIndex}' is greater or equals to the total stream count of {multiStreamProperties.Count}.");
                return;
            }
            
            SelectCaptionTrack(streamIndex, ccTrackIndex);
        }
    }
}