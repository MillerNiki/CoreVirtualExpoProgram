//.NetSystemCollections
using System.Collections;
using System.Collections.Generic;
using System.Linq;

//Unity Library
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//PhotonPunLibrarySDK
using Photon.Pun;
using Photon.Realtime;

namespace VirtualExpo.MainArea.PlayerUICustomized
{

    /// Color Note : 
    /// - Skyblue : Connection Granted (#87ceeb)
    /// - NeonGreen : Joined room / Joined to master (#39ff14)
    /// - Orange : left room / Disconnected (#FFA500)
    /// - NeonViolet : Joining Room Scene (#B026FF)
    /// - NeonYellow : Ping Connection (#FFF01F)
    /// - Snow : RoomUpdate

    public class PlayerUI : MonoBehaviourPunCallbacks
    {

        #region Public Variable

        [Header("UI TextMeshPro")]
        public TMP_Text playerNameText;

        #endregion

        #region Private Variable

        private string playerName;
        private string playerID;

        #endregion

        private void Update()
        {

            playerNameText.text = playerName + "\n" + playerID;

        }

        public void DefaultUI(string name, string id)
        {

            playerName = name;
            playerID = id;

        }

        public void DefaultUI(string name)
        {

            playerName = name;

        }


    }

}