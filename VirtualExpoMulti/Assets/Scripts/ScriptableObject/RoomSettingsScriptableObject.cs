//Unity Library
using UnityEngine;

namespace VirtualExpo.CustomRoomsSettings
{

    [CreateAssetMenu(fileName = "Custom Room", menuName = "New Custom Room")]
    public class RoomSettingsScriptableObject : ScriptableObject
    {

        [Space(5)]
        [Header("Custom Room Variable")]
        [SerializeField] internal string roomName;
        [SerializeField] internal byte maxPlayers;
        [SerializeField] internal bool isVisible;
        [SerializeField] internal bool isOpen;
        [SerializeField] internal bool isPublishUserId;

    }

}