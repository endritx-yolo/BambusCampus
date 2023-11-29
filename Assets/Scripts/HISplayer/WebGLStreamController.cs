using System;
using System.Collections;
using System.IO;
using System.Net;
using HISPlayerAPI;
using UnityEngine;
using UnityEngine.Networking;

namespace VideoStreaming
{
    public class WebGLStreamController : HISPlayerManager
    {
        private static WebGLStreamController _instance;
        public static WebGLStreamController Instance => _instance;
        
        private const string _streamFileName = "StreamLink.txt";

        private VideoStreamingProperties _videoStreamingProperties;

        private string _result = String.Empty;
        private bool _isStreamSetUp;
        
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
             StartCoroutine(ReadFile(SetUpHisPlayer));
        }
        
        private IEnumerator ReadFile(Action callback)
        {
            string filePath = Path.Combine(Application.streamingAssetsPath, _streamFileName);
            UnityWebRequest webRequest = UnityWebRequest.Get(filePath);
            yield return webRequest.SendWebRequest();
            _result = webRequest.downloadHandler.text;;
            callback?.Invoke();
        }

        private void SetUpHisPlayer()
        {
            if (_result != String.Empty)
                for (int i = 0; i < multiStreamProperties.Count; i++)
                    multiStreamProperties[i].url[0] = _result;
            
            SetUpPlayer();
            _isStreamSetUp = true;
        }

        private void Start() => SetNewVolume(1f);

#if UNITY_EDITOR
        private void OnApplicationQuit() => Release();
#endif

        public void PlayStream(int streamIndex)
        {
            if (!_isStreamSetUp) return;
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
            if (!_isStreamSetUp) return;
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
            if (!_isStreamSetUp) return;
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
            if (!_isStreamSetUp) return;
            for (int i = 0; i < multiStreamProperties.Count; i++)
                Stop(i);
        }

        public void SetNewVolume(float volume)
        {
            if (!_isStreamSetUp) return;
            volume = Mathf.Clamp(volume, 0f, 1f);
            for (int i = 0; i < multiStreamProperties.Count; i++)
                SetVolume(i, volume);
        }

        public void SetNewVolume(int streamIndex, float volume)
        {
            if (!_isStreamSetUp) return;
            if (streamIndex >= multiStreamProperties.Count)
            {
                Debug.LogError(
                    $"Stream index '{streamIndex}' is greater or equals to the total stream count of {multiStreamProperties.Count}.");
                return;
            }

            SetVolume(streamIndex, volume);
        }

        public void ChangeSpeedRate(float newSpeed)
        {
            if (!_isStreamSetUp) return;
            for (int i = 0; i < multiStreamProperties.Count; i++)
                SetPlaybackSpeedRate(i, newSpeed);
        }

        public void ToggleSubtitles(int streamIndex, int ccTrackIndex)
        {
            if (!_isStreamSetUp) return;
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