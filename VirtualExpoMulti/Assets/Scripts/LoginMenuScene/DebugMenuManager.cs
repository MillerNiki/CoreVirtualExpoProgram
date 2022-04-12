//.NetSystemCollections
using System.Collections;
using System.Collections.Generic;
//Unity Library
using UnityEngine;
using TMPro;

//PhotonPunLibrarySDK
using Photon.Pun;
using Photon.Realtime;

namespace VirtualExpo.MainMenu.DebugLogManager
{

    /// Color Note : 
    /// - Skyblue : Connection Granted (#87ceeb)
    /// - NeonGreen : Joined room / Joined to master (#39ff14)
    /// - Orange : left room / Disconnected (#FFA500)
    /// - NeonViolet : Joining Room Scene (#B026FF)
    /// - NeonYellow : Ping Connection (#FFF01F)

    public class DebugMenuManager : MonoBehaviour
    {

        #region Public Variable

        [Header("UI Elements")]
        public TMP_Text debugModeText;

        #endregion
        #region Private Variable
        [Space(5)]
        [Header("MainPrivateVariable")]
        [SerializeField]private int avgFrameRate;
        public int FrameRate
        {
            get { return avgFrameRate; }
            set { avgFrameRate = value; }
        }

        bool isActived = false;

        #endregion

        #region Unity Method(s)

        #region Core Method(s)

        private void Awake()
        {

            debugModeText.gameObject.SetActive(false);

        }

        private void Update()
        {

            EnableFPSCounter();
            
        }

        private void FixedUpdate()
        {

            FPSCounter();
            if (PhotonNetwork.IsConnectedAndReady)
            {
                PingRateSpeed();
            }

        }

        #endregion

        #region FPSProfiler

        void EnableFPSCounter()
        {

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                isActived = !isActived;
            }

            if (isActived == true)
            {

                debugModeText.gameObject.SetActive(true);

            }
            else
            {
                debugModeText.gameObject.SetActive(false);
            }

        }

        void FPSCounter()
        {

            float current = 0;
            current = (int)(1f / Time.unscaledDeltaTime);
            avgFrameRate = (int)current;
            debugModeText.text = avgFrameRate.ToString() + " FPS";

        }

        #endregion

        #region PingRate

        void PingRateSpeed()
        {

            debugModeText.text += "\nPing : " + PhotonNetwork.GetPing() + " /ms";

        }

        #endregion

        #region GPUProfiler



        #endregion

        #endregion

    }

}
