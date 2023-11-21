using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using HISPlayerAPI;
using UnityEngine;
using UnityEngine.Networking;

namespace VideoStreaming
{
    public class WebGLStreamController : HISPlayerManager
    {
        private static WebGLStreamController _instance;
        public static WebGLStreamController Instance => _instance;

        private const string _staticPath = "D:\\Unity Repos\\BambusCampus\\Builds\\StreamingAssets\\properties.json";

        private const string _urlPath =
            @"ftp://endrit.xhemaili%2540yolo-way.com@92.205.8.194/unity_showrooms/bambus_hisplayer_showroom/livestream.txt";

        private VideoStreamingProperties _videoStreamingProperties;

        protected override void Awake()
        {
            //ReadJsonFile();
            //byte[] byteArray = ReadUrlHTTPRequest();
            //string textstring = Convert.ToBase64String(text);
            
            string result = ReadUrlHTTPRequest();

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
            SetUpPlayer();
        }

        private void Start() => SetNewVolume(1f);

        private void ReadJsonFile()
        {
            string filePath = Path.Combine(Application.streamingAssetsPath, "properties.json");
#if UNITY_WEBGL
            filePath = filePath.Replace("/", "");
            filePath = _staticPath;
#endif
            string dataAsJson = File.ReadAllText(filePath);
            _videoStreamingProperties = JsonUtility.FromJson<VideoStreamingProperties>(dataAsJson);
        }

        private string ReadUrlHTTPRequest()
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(_urlPath));

            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = true;

            request.Credentials = new NetworkCredential("endrit.xhemaili@yolo-way.com", "k2){^Kp=^}pN");
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            return DownloadAsByteArray(request.GetResponse());

            /*UnityWebRequest myWr = UnityWebRequest.Get(_urlPath);
            yield return myWr.SendWebRequest();

            if (myWr.result != UnityWebRequest.Result.Success)
            {
                Debug.LogWarning(myWr.error);
                yield break;
            }
            Debug.LogWarning(myWr.downloadHandler.text);
            byte[] results = myWr.downloadHandler.data;
            for (int i = 0; i < results.Length; i++)
            {
                Debug.LogWarning(results[i].ToString());
            }*/
        }

        private string DownloadAsByteArray(WebResponse request)
        {
            using (Stream input = request.GetResponseStream())
            {
                byte[] buffer = new byte[16 * 1024];
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

        public void SetNewVolume(int streamIndex, float volume)
        {
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