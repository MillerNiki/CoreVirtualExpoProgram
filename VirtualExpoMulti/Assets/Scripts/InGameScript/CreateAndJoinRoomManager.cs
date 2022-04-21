//.NetSystemCollections
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

//Unity Library
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

//PhotonPunLibrarySDK
using Photon.Pun;
using Photon.Realtime;

using VirtualExpo.MainArea.PlayerSpawnManager;
using VirtualExpo.CustomRoomsSettings;


namespace VirtualExpo.MainArea.RoomManager
{

    /// Color Note : 
    /// - Skyblue : Connection Granted (#87ceeb)
    /// - NeonGreen : Joined room / Joined to master (#39ff14)
    /// - Orange : left room / Disconnected (#FFA500)
    /// - NeonViolet : Joining Room Scene (#B026FF)
    /// - NeonYellow : Ping Connection (#FFF01F)
    /// - Snow : RoomUpdate

    public class CreateAndJoinRoomManager : MonoBehaviourPunCallbacks
    {

        #region Public Variable

        RoomListManager roomListManager;
        SpawnManager spManager;

        #endregion

        #region Private Variable

        [Space(5)]
        [Header("Custom Room Variable")]

        [SerializeField] RoomSettingsScriptableObject[] roomSettings;

        bool isJoinedroom = false;
        public bool isJoin
        {

            get { return isJoinedroom; }
            set
            {
                isJoinedroom = value;
            }

        }

        bool isLeftRoom = false;
        public bool isLeft
        {
            get { return isLeftRoom; }
            set { isLeftRoom = value; }
        }
        
        #endregion

        #region Unity Method(s)

        #region Unity Core Method(s)

        private void Awake()
        {

            roomListManager = GetComponent<RoomListManager>();
            spManager = GetComponent<SpawnManager>();

            PhotonNetwork.AutomaticallySyncScene = true;

            roomSettings = Resources.LoadAll("DataSettings/RoomSettings", typeof(RoomSettingsScriptableObject)).Cast<RoomSettingsScriptableObject>().ToArray();// for many data
            //roomSettings = Resources.Load<RoomSettingsScriptableObject>("DataSettings/RoomSettings/MainArea01");//for single data

        }

        private void Update()
        {
            
            if (PhotonNetwork.IsConnectedAndReady)
            {
                Debug.Log("Lobby Name : " + PhotonNetwork.CurrentLobby.Name);
                CreateOrJoinRoom();
            }

        }

        #endregion

        #region Create or Join Room Manager

        private void CreateRoomNow(string roomName, bool isVisible, bool isOpen, byte maxPlayer, bool isPublishUserId)
        {
            
            if (PhotonNetwork.IsConnectedAndReady)
            {

                if (!isJoinedroom)
                {

                    RoomOptions roomOptions = new RoomOptions();
                    roomOptions.IsVisible = isVisible;
                    roomOptions.IsOpen = isOpen;
                    roomOptions.MaxPlayers = maxPlayer;
                    roomOptions.PublishUserId = isPublishUserId;
                    //roomOptions.CustomRoomProperties = customRoomProperties;
                    //roomOptions.CustomRoomPropertiesForLobby = propsToListInLobby;
                    PhotonNetwork.CreateRoom(roomName, roomOptions);

                }
                else
                {
                    Debug.Log("You has been joined room! Room Name : " + PhotonNetwork.CurrentRoom.Name);
                }

            }

            return;

        }

        private void JoinRoom(string roomName)
        {

            if (PhotonNetwork.IsConnectedAndReady)
            {
                PhotonNetwork.JoinRoom(roomName);
            }

        }

        public void CreateOrJoinRoom()
        {

            ///check if we're in room or not
            if (!isJoinedroom)
            {

                /**if (PhotonNetwork.CountOfRooms == 0)
                {

                    CreateRoomNow(roomSettings.roomName, roomSettings.isVisible, roomSettings.isOpen, roomSettings.maxPlayers, roomSettings.isPublishUserId);

                }
                else
                {

                    JoinRoom(roomSettings.roomName); 

                }**/

                for (int i = 0; i < roomSettings.Length; i++)
                {
                    string currentSceneName = SceneManager.GetActiveScene().name;

                    if (roomSettings[i].name == currentSceneName)
                    {
                        JoinRoom(roomSettings[i].roomName);
                    }

                }

            }
            else
            {
                Debug.Log("You've already joined room");
                return;
            }            

        }

        #endregion



        #endregion

        #region PhotonPun Callback(s)

        //Called when this client created a room and entered it. OnJoinedRoom() will be called as well.
        public override void OnCreatedRoom()
        {
            Debug.Log("<color=#B026FF>New room has been created ! </color>");
        }

        //Called when the LoadBalancingClient entered a room, no matter if this client created it or simply joined.
        public override void OnJoinedRoom()
        {

            Debug.Log("<color=#B026FF>Joined Room ! Room Name : " + PhotonNetwork.CurrentRoom.Name + ", MaxPlayers : " + PhotonNetwork.CurrentRoom.MaxPlayers + "</color>");

            isJoinedroom = true;

            spManager.SpawningPlayerNow();

        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {

            Debug.LogWarning("<color=#FFA500>Message :  " + message + "</color>");

            for (int i = 0; i < roomSettings.Length; i++)
            {
                string currentSceneName = SceneManager.GetActiveScene().name;

                if (roomSettings[i].name == currentSceneName)
                {
                    CreateRoomNow(roomSettings[i].roomName, roomSettings[i].isVisible, roomSettings[i].isOpen, roomSettings[i].maxPlayers, roomSettings[i].isPublishUserId);
                }

            }

        }

        //Called when the local user/client left a room, so the game's logic can clean up it's internal state.
        public override void OnLeftRoom()
        {

            Debug.Log("<color=#FFA500>You was left this room !</color>");
            isJoinedroom = false;

            if (isLeftRoom)
            {
                //back to launcher
            }

        }

        #endregion

    }

}