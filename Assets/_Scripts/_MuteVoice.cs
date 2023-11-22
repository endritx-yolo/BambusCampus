using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Voice.Unity;

namespace Fusion.KCC
{
    public class _MuteVoice : MonoBehaviour
    {
        // Name of the GameObject containing the Recorder script
        public string recorderGameObjectName = "RecorderGameObject";

        // Reference to the Recorder script
        private Recorder recorder;

        // Start is called before the first frame update
        void Start()
        {
            // Find the GameObject with the specified name
            GameObject recorderGameObject = GameObject.Find(recorderGameObjectName);

            if (recorderGameObject != null)
            {
                // Get the Recorder script component from the GameObject
                recorder = recorderGameObject.GetComponent<Recorder>();

                if (recorder == null)
                {
                    Debug.LogError("Recorder script not found on the GameObject: " + recorderGameObjectName);
                }
            }
            else
            {
                Debug.LogError("GameObject with name " + recorderGameObjectName + " not found in the scene.");
            }
        }

        // Update is called once per frame
        void Update()
        {
            // Check for user input or some other condition to toggle recordingEnabled
            if (Input.GetKeyDown(KeyCode.M))
            {
                // Toggle the recordingEnabled variable
                if (recorder != null)
                {
                    recorder.recordingEnabled = !recorder.recordingEnabled;
                    Debug.Log("Recording Enabled: " + recorder.recordingEnabled);
                }
                else
                {
                    Debug.LogError("Recorder script reference is null.");
                }
            }
        }
    }
}
