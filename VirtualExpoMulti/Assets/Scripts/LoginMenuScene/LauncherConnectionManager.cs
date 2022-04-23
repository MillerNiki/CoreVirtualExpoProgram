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

using VirtualExpo.CustomLobbySettings;

namespace VirtualExpo.MainMenu.ConnectionManager
{

    /// Color Note : 
    /// - Skyblue : Connection Granted (#87ceeb)
    /// - NeonGreen : Joined room / Joined to master (#39ff14)
    /// - Orange : left room / Disconnected (#FFA500)
    /// - NeonViolet : Joining Room Scene (#B026FF)
    /// - NeonYellow : Ping Connection (#FFF01F)
    /// - Snow : RoomUpdate

    public class LauncherConnectionManager : MonoBehaviourPunCallbacks
    {

        #region Public Variable
        [Header("UI Elements")]
        public TMP_InputField inputNickName;
        public TMP_Dropdown dropdownMenu;
        public Button startButton;

        #endregion
        #region Private Variable
        [Space(5)]
        [Header("Photon Player Identity Variable")]
        [SerializeField] private string playerNickName;
        [SerializeField] private string appVersion = "1";
        [SerializeField] private int charSelection;

        [SerializeField] private LobbySettingsScriptableObject lobbySettings;

        [SerializeField] private string sceneName = "MainArea";


        LobbyType lobbyTypeChoise;
        TypedLobby customLobby;

        #endregion

        #region Unity Method(s)

        #region Unity Core Method(s)

        private void Awake()
        {
            
            PhotonNetwork.AutomaticallySyncScene = true;

            //Resources.LoadAll("FolderName/FolderName(x)", typeof("TypeOfClass/Data")).Cast<"TypeOfClass/Data">().ToArray(); // this is for load the data from resources

        }

        private void Update()
        {

            PlayerNameInput();
       
        }

        #endregion

        #region PlayerNamingSystem

        public void ConnectToServer()
        {

            //PlayerNameInput();
            // we check if we are connected or not, we join if we are , else we initiate the connection to the server.
            if (!PhotonNetwork.IsConnected)
            {
                ConnectionSettings();
            }

        }

        private void ConnectionSettings()
        {

            // #Critical, we must first and foremost connect to Photon Online Server.
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = this.appVersion; //Application Version
            PhotonNetwork.NickName = this.playerNickName; //settingPlayer NickName

            Debug.Log(this.playerNickName + ", " + this.charSelection);

        }

        private void PlayerNameInput()
        {

            if (inputNickName.text == "")
            {

                startButton.interactable = false;
                //Debug.LogWarning("Nickname field is empty.");

            }
            else
            {

                startButton.interactable = true;
                playerNickName = inputNickName.text;
                charSelection = dropdownMenu.value;
                PlayerPrefs.SetInt("CharacterSelection", this.charSelection);

            }

        }

        #endregion

        #region Custom Lobby Manager

        private void JoinCustomLobby()
        {

            switch (lobbySettings.typeListLobby)
            {
                case "Default":
                    lobbyTypeChoise = LobbyType.Default;
                    Debug.Log("<color=yellow>Default LobbyType</color>");
                    break;
                case "AsyncRandomLobby":
                    lobbyTypeChoise = LobbyType.AsyncRandomLobby;
                    Debug.Log("<color=yellow>AsyncRandomLobby LobbyType</color>");
                    break;
                case "SqlLobby":
                    lobbyTypeChoise = LobbyType.SqlLobby;
                    Debug.Log("<color=yellow>SqlLobby LobbyType</color>");
                    break;
            }

            customLobby = new TypedLobby(lobbySettings.lobbyName, lobbyTypeChoise);

            if (PhotonNetwork.IsConnectedAndReady)
            {
                PhotonNetwork.JoinLobby(customLobby);
            }

        }

        public void LeftLobby()
        {

            PhotonNetwork.LeaveLobby();

        }

        #endregion
        
        #endregion

        #region PHOTONPUN_CALLBACK_METHOD(S)
        //Called to signal that the raw connection got established but before the client can call operation on the server.
        public override void OnConnected()
        {

            Debug.Log("<color=#87ceeb>Server Detected. You're Connected to server! \n Region : " + PhotonNetwork.CloudRegion + " (Raw Connection)(OnConnected()Method)</color>");
            
        }

        //Called when the client is connected to the Master Server and ready for matchmaking and other tasks.
        public override void OnConnectedToMaster()
        {

            Debug.Log("<color=#87ceeb>You're Connected to Master Server ! \n Player NickName : " + PhotonNetwork.NickName + " </color>");
            
            if (PhotonNetwork.IsConnectedAndReady)
            {
                JoinCustomLobby();
            }

        }

        //Called on entering a lobby on the Master Server. The actual room-list updates will call OnRoomListUpdate.
        public override void OnJoinedLobby()
        {

            Debug.Log("<color=#B026FF>Welcome to lobby " + PhotonNetwork.NickName + ", \n Now @ Lobby : " + PhotonNetwork.CurrentLobby.Name + "</color>");

            if (PhotonNetwork.IsConnectedAndReady)
            {
                PlayerPrefs.SetString("TargetScene", sceneName);
                PhotonNetwork.LoadLevel("LoadingScene");
            }

        }

        //When you leave a lobby, OpCreateRoom and OpJoinRandomRoom automatically refer to the default lobby.
        public override void OnLeftLobby()
        {

            Debug.Log("<color=#B026FF>" + PhotonNetwork.NickName + ", \n has left the Lobby, now at Master Server!</color>");

        }

        //Called after disconnecting from the Photon server. It could be a failure or intentional
        public override void OnDisconnected(DisconnectCause cause)
        {
            
            Debug.Log("<color=#FFA500>You has been disconnected from server!</color>");
        
        }


        #endregion

    }

}