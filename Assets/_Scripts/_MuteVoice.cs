using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Voice.Unity;

namespace Fusion.KCC
{
    public class _MuteVoice : MonoBehaviour
    {
        // Name of the GameObjects containing the Recorder script
        public string recorderGameObjectName = "Recorder";

        // List to store all found Recorder scripts
        private List<Recorder> recorders = new List<Recorder>();

        // Start is called before the first frame update
        void Start()
        {
            // Find all GameObjects with the specified name
            GameObject[] recorderGameObjects = GameObject.FindObjectsOfType<GameObject>();

            foreach (GameObject recorderGameObject in recorderGameObjects)
            {
                // Check if the GameObject's name matches the specified name
                if (recorderGameObject.name == recorderGameObjectName)
                {
                    // Get the Recorder script component from each GameObject
                    Recorder foundRecorder = recorderGameObject.GetComponent<Recorder>();

                    if (foundRecorder != null)
                    {
                        recorders.Add(foundRecorder);
                    }
                    else
                    {
                        Debug.LogError("Recorder script not found on the GameObject: " + recorderGameObject.name);
                    }
                }
            }

            if (recorders.Count == 0)
            {
                Debug.LogError("No Recorder scripts found with the name: " + recorderGameObjectName);
            }
        }

        // Update is called once per frame
        void Update()
        {
            // Check for user input or some other condition to toggle recordingEnabled for all found Recorders
            if (Input.GetKeyDown(KeyCode.M))
            {
                // Toggle the recordingEnabled variable for all found Recorders
                foreach (Recorder recorder in recorders)
                {
                    if (recorder != null)
                    {
                        recorder.RecordingEnabled = !recorder.RecordingEnabled;
                        Debug.Log("Recording Enabled: " + recorder.RecordingEnabled + " for Recorder: " + recorder.gameObject.name);
                    }
                    else
                    {
                        Debug.LogError("Recorder script reference is null.");
                    }
                }
            }
        }
    }
}
