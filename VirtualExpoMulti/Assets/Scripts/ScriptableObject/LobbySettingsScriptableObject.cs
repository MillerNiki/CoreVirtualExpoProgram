//Unity Library
using UnityEngine;

namespace VirtualExpo.CustomLobbySettings
{


    [CreateAssetMenu(fileName = "Custom Lobby", menuName = "New Custom Lobby")]
    public class LobbySettingsScriptableObject : ScriptableObject
    {

        [Space(5)]
        [Header("Lobby Variable")]
        [SerializeField] internal string lobbyName;
        enum lobbyTypeList
        {

            Default,
            AsyncRandomLobby,
            SqlLobby

        }
        [SerializeField] private lobbyTypeList lobbyType;
        public string typeListLobby
        {
            get { return lobbyType.ToString(); }
        }

    }


}