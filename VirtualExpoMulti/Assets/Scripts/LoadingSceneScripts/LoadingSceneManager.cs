//.NetSystemCollections
using System.Collections;
using System.Collections.Generic;
using System.Linq;

//Unity Library
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//PhotonPunLibrarySDK
using Photon.Pun;
using Photon.Realtime;

using VirtualExpo.CustomLobbySettings;


namespace VirtualExpo.LoadingScene
{

    /// Color Note : 
    /// - Skyblue : Connection Granted (#87ceeb)
    /// - NeonGreen : Joined room / Joined to master (#39ff14)
    /// - Orange : left room / Disconnected (#FFA500)
    /// - NeonViolet : Joining Room Scene (#B026FF)
    /// - NeonYellow : Ping Connection (#FFF01F)
    /// - Snow : RoomUpdate

    public class LoadingSceneManager : MonoBehaviourPunCallbacks
    {

        // Reference to the load operation.
        private AsyncOperation loadOperation;

        // Reference to the progress bar in the UI.
        [SerializeField]
        private Slider progressBar;

        // Progress values.
        private float currentValue;
        private float targetValue;

        // Multiplier for progress animation speed.
        [SerializeField]
        [Range(0, 1)]
        private float progressAnimationMultiplier = 0.25f;

        private static string sceneName;

        private void Start()
        {

            //PhotonNetwork.AutomaticallySyncScene = true;

            Debug.Log(PhotonNetwork.CurrentLobby.Name);

            // Set 0 for progress values.
            progressBar.value = currentValue = targetValue = 0;

            // Load the target scene.
            sceneName = PlayerPrefs.GetString("TargetScene");
            loadOperation = SceneManager.LoadSceneAsync(sceneName);

            // Don't active the scene when it's fully loaded, let the progress bar finish the animation.
            // With this flag set, progress will stop at 0.9f.
            loadOperation.allowSceneActivation = false;

        }

        private void Update()
        {
            
            LoadingAnimation();

        }

        void LoadingAnimation()
        {

            // Assign current load progress, divide by 0.9f to stretch it to values between 0 and 1.
            targetValue = loadOperation.progress / 0.9f;

            // Calculate progress value to display.
            currentValue = Mathf.MoveTowards(currentValue, targetValue, progressAnimationMultiplier * Time.deltaTime);
            progressBar.value = currentValue;

            // When the progress reaches 1, allow the process to finish by setting the scene activation flag.
            if (Mathf.Approximately(currentValue, 1))
            {
                loadOperation.allowSceneActivation = true;
            }

        }

    }


}

