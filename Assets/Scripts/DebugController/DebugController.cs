using System;
using System.Collections.Generic;
using Leon;
using UnityEngine;
using VideoStreaming;

namespace InGameDebugging
{
    public class DebugController : SceneSingleton<DebugController>
    {
        private bool _showConsole;
        private bool _showHelp;
        private string _input;

        public static DebugCommand HELP;
        public static DebugCommand STOP_ALL_LIVESTREAMS;
        public static DebugCommand<float> SET_STREAM_VOLUME;
        public static DebugCommand<float> SET_SPEED_RATE;
        public static DebugCommand<int, string> CHANGE_STREAM_URL;
        public static DebugCommand<int, int> SET_STREAM_CC;

        private Vector2 _scroll;

        public List<object> _commandList;

        private void Awake()
        {
            HELP = new DebugCommand("help",
                "Show a list of all available commands.", "help",
                () => { _showHelp = true; });

            STOP_ALL_LIVESTREAMS = new DebugCommand("stop_all_livestreams", "Stops all livestreams from playing.",
                "stop_all_livestreams",
                () =>
                {
                    WebGLStreamController.Instance.StopAllLiveStreams();
                    Debug.Log($"Stopped all livestreams.");
                });

            SET_STREAM_VOLUME = new DebugCommand<float>("set_stream_volume",
                "Sets the stream volume to a specified value.", "set_stream_volume",
                (x) =>
                {
                    WebGLStreamController.Instance.SetNewVolume(x);
                    Debug.Log($"Livestream volume is now set at {x}.");
                });

            SET_SPEED_RATE = new DebugCommand<float>("set_speed_rate",
                "Sets the playback speed rate to a specified value.", "set_speed_rate",
                (x) =>
                {
                    WebGLStreamController.Instance.ChangeSpeedRate(x);
                    Debug.Log($"Livestream playback speed rate is set to {x}.");
                });

            CHANGE_STREAM_URL = new DebugCommand<int, string>("change_stream_url",
                "Change the url of a video stream.", "change_stream_url",
                (x, y) =>
                {
                    WebGLStreamController.Instance.SetNewTrackURL(x, y);
                    Debug.Log($"Changed url for stream with index {x} stream to {y}.");
                });

            SET_STREAM_CC = new DebugCommand<int, int>("set_stream_cc",
                "Sets the captions of a video stream.", "set_stream_cc",
                (x, y) =>
                {
                    WebGLStreamController.Instance.ToggleSubtitles(x, y);
                    Debug.Log($"Set the captions for stream with index {x} to cc with index {y}.");
                });

            _commandList = new List<object>
            {
                HELP,
                STOP_ALL_LIVESTREAMS,
                CHANGE_STREAM_URL,
                SET_STREAM_VOLUME,
                SET_SPEED_RATE,
                SET_STREAM_CC
            };
        }

        public void ToggleDebug() => _showConsole = !_showConsole;

        public void OnHitReturn()
        {
            if (!_showConsole) return;
            HandleInput();
            _input = "";
        }

        private void OnGUI()
        {
            if (!_showConsole)
            {
                _input = String.Empty;
                return;
            }

            float y = 0f;
            string console = "console";

            if (_showHelp)
            {
                GUI.Box(new Rect(0, y, Screen.width, 150), "");
                Rect viewport = new Rect(0, 0, Screen.width - 30, 20 * _commandList.Count);
                _scroll = GUI.BeginScrollView(new Rect(0, y + 5f, Screen.width, 140), _scroll, viewport);
                for (int i = 0; i < _commandList.Count; i++)
                {
                    DebugCommandBase command = _commandList[i] as DebugCommandBase;
                    string label = $"{command.CommandFormat} - {command.CommandDescription}";
                    Rect labelRect = new Rect(5, 20 * i, viewport.width - 100, 20);
                    GUI.Label(labelRect, label);
                }

                GUI.EndScrollView();
                y += 150f;
            }

            GUI.Box(new Rect(0, y, Screen.width, 30), "");
            GUI.backgroundColor = new Color(0, 0, 0, 0);
            GUI.SetNextControlName(console);
            _input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), _input);
            GUI.FocusControl(console);
            _input = _input.Replace("`", "");
        }

        private void HandleInput()
        {
            string[] properties = _input.Split(' ');

            for (int i = 0; i < _commandList.Count; i++)
            {
                DebugCommandBase commandBase = _commandList[i] as DebugCommandBase;
                if (!_input.Contains(commandBase.CommandId)) continue;

                if (_commandList[i] as DebugCommand != null)
                    (_commandList[i] as DebugCommand).Invoke();

                if (_commandList[i] as DebugCommand<string> != null)
                    (_commandList[i] as DebugCommand<string>).Invoke(properties[1]);

                if (_commandList[i] as DebugCommand<float> != null)
                    (_commandList[i] as DebugCommand<float>).Invoke(float.Parse(properties[1]));

                if (_commandList[i] as DebugCommand<int, string> != null)
                    (_commandList[i] as DebugCommand<int, string>).Invoke(int.Parse(properties[1]), properties[2]);

                if (_commandList[i] as DebugCommand<int, int> != null)
                    (_commandList[i] as DebugCommand<int, int>).Invoke(int.Parse(properties[1]),
                        int.Parse(properties[2]));
            }
        }
    }
}