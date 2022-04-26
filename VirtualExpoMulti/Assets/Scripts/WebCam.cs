using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;
using TMPro;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

namespace VirtualExpo.WebCamHardware
{

    /*
     For those who are troubling with zoom in front facing camera, thanks to 歐陽東林 for the idea:
     background.rectTransform.localScale = new Vector3(1f * ratio, scaleY * ratio,  1f * ratio); 
     Then set aspect fitter to fit parent.
     */

    public class WebCam : MonoBehaviourPunCallbacks
    {

        private bool camAvailable;
        private WebCamTexture realCamera;
        private Texture defaultBackground;

        public RawImage background;
        public AspectRatioFitter fitter;


        private void Awake()
        {

            defaultBackground = background.texture;
            WebCamDevice[] devices = WebCamTexture.devices;

            if (devices.Length == 0)
            {
                Debug.Log("No WebCam Found.");
                camAvailable = false;
                return;
            }

            IFCameraDetected(devices);

        }

        void IFCameraDetected(WebCamDevice[] device)
        {

            for (int i = 0; i < device.Length; i++)
            {

                if (device[i].isFrontFacing)
                {

                    realCamera = new WebCamTexture(device[i].name, Screen.width, Screen.height);

                }

            }

            if (realCamera == null)
            {
                Debug.Log("Unable To Find Camera");
                return;
            }

            realCamera.Play();
            background.texture = realCamera;

            camAvailable = true;

        }

        private void Update()
        {

            UpdateCameraStatus();

        }

        void UpdateCameraStatus()
        {

            if (!camAvailable)
            {
                return;
            }

            float ratio = (float)realCamera.width / (float)realCamera.height;
            fitter.aspectRatio = ratio;

            float scaleY = realCamera.videoVerticallyMirrored ? -1f : 1f;
            background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

            int orient = -realCamera.videoRotationAngle;
            background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);

        }

    }

}