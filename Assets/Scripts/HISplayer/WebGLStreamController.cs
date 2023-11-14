using System.IO;
using HISPlayerAPI;
using UnityEngine;

namespace VideoStreaming
{
    public class WebGLStreamController : HISPlayerManager
    {
        private static WebGLStreamController _instance;
        public static WebGLStreamController Instance => _instance;

        private VideoStreamingProperties _videoStreamingProperties;

        protected override void Awake()
        {
            ReadJsonFile();

            if (_instance == null)
                _instance = this;
            else
            {
                Destroy(gameObject);
                return;
            }

            for (int i = 0; i < multiStreamProperties.Count; i++)
                multiStreamProperties[i].url[0] = _videoStreamingProperties.videoStreamingProperties[i].url;

            base.Awake();
            SetUpPlayer();
        }

        private void ReadJsonFile()
        {
            string filePath = Path.Combine(Application.streamingAssetsPath, "properties.json");
            string dataAsJson = File.ReadAllText(filePath);
            Debug.LogWarning(dataAsJson);
            if (!File.Exists(filePath)) return;
            _videoStreamingProperties = JsonUtility.FromJson<VideoStreamingProperties>(dataAsJson);
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