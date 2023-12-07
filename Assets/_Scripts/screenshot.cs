using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Rendering;

namespace Fusion.KCC
{
    
    public class screenshot : MonoBehaviour
    {

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                StartCoroutine(TakeScreenshot());
            }
        }

        IEnumerator TakeScreenshot()
        {
            yield return new WaitForEndOfFrame();

            string fileName = "screenshot-" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".png";

            // Capture the screenshot
            Texture2D screenTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            screenTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            screenTexture.Apply();

            // Convert the texture to bytes
            byte[] bytes = screenTexture.EncodeToPNG();

            // Convert bytes to base64 string
            string base64String = Convert.ToBase64String(bytes);

            // Pass the base64 string to JavaScript
            string javaScriptCode = @"
                var link = document.createElement('a');
                link.href = 'data:image/png;base64," + base64String + @"';
                link.download = '" + fileName + @"';
                document.body.appendChild(link);
                link.click();
                document.body.removeChild(link);
            ";

            // Execute the JavaScript
            Application.ExternalEval(javaScriptCode);
        }
    }

}

