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

        private const string _ftpUrlPath =
            @"ftp://endrit.xhemaili%2540yolo-way.com@92.205.8.194/unity_showrooms/bambus_hisplayer_showroom/livestream.txt";

        private const string _httpUrlPath = @"https://57ae-84-22-36-74.ngrok-free.app";
        
        private VideoStreamingProperties _videoStreamingProperties;

        [SerializeField] private RequestType _requestType = RequestType.FTP;

        private string _result = String.Empty;
        private bool _isStreamSetUp;
        
        protected override void Awake()
        {
            /*string result = ReadUrlFTPRequest();
            if (_instance == null)
                _instance = this;
            else
            {
                Destroy(gameObject);
                return;
            }
            
            for (int i = 0; i < multiStreamProperties.Count; i++)
                multiStreamProperties[i].url[0] = result;
            
            base.Awake();
            SetUpPlayer();*/
            
             if (_instance == null)
                _instance = this;
            else
            {
                Destroy(gameObject);
                return;
            }
             base.Awake();

            /*switch (_requestType)
            {
                case RequestType.FTP:
                    _result = ReadUrlFTPRequest();
                    Debug.LogError(_result);
                    break;
                case RequestType.HTTP:
                    StartCoroutine(ReadUrlHTTPRequest(SetUpHisPlayer));
                    return;
                    //_result = ReadUrlHTTPWebRequest();
                    break;
                case RequestType.None:
                    break;
            }*/

            SetUpHisPlayer();
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

        private string ReadUrlFTPRequest()
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(_ftpUrlPath));

            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = true;

            request.Credentials = new NetworkCredential("endrit.xhemaili@yolo-way.com", "k2){^Kp=^}pN");
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            try
            {
                var response = request.GetResponse();
                return DownloadAsByteArray(response);
            }
            catch (WebException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private IEnumerator ReadUrlHTTPRequest(Action callback)
        {
            string urlPath = $"{_httpUrlPath}/livestreamlink/LivestreamUrl.txt";
            UnityWebRequest webRequest = UnityWebRequest.Get(urlPath);
            webRequest.SetRequestHeader("Access-Control-Allow-Credentials", "true");
            webRequest.SetRequestHeader("Access-Control-Expose-Headers", "Content-Length, Content-Encoding");
            webRequest.SetRequestHeader("Access-Control-Allow-Headers", "Accept, X-Access-Token, X-Application-Name, X-Request-Sent-Time, Content-Type");
            webRequest.SetRequestHeader("Access-Control-Allow-Method", "GET, POST, OPTIONS");
            webRequest.SetRequestHeader("Access-Control-Allow-Origin", "*");
            yield return webRequest.SendWebRequest();
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogWarning(webRequest.error);
            }
            else
            {
                _result = webRequest.downloadHandler.text;
                Debug.LogWarning("Stream Link: " + _result);
                callback?.Invoke();
            }

            webRequest.Dispose();
        }

        private string ReadUrlHTTPWebRequest()
        {
            string strContent = String.Empty;
            string urlPath = $"{_httpUrlPath}/livestreamlink/LivestreamUrl.txt";
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(new Uri(urlPath));

            var response = webRequest.GetResponse();
            var content = response.GetResponseStream();
            
            return DownloadAsByteArray(webRequest.GetResponse());
            
            using (var reader = new StreamReader(content))
            {
                strContent = reader.ReadToEnd();
                return strContent;
            }
        }

        private string DownloadAsByteArray(WebResponse request)
        {
            using (Stream input = request.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(input))
                {
                    string fileContent = reader.ReadToEnd();
                    return fileContent;
                }
            }
        }


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