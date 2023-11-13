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
                Destroy(gameObject);
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
    }
}