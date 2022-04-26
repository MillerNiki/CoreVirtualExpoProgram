//.NetSystemCollections
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

//Unity Library
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//PhotonPunLibrarySDK
using Photon.Pun;
using Photon.Realtime;

using VirtualExpo.Player;
using VirtualExpo.CharacterDatas;

namespace VirtualExpo.MainArea.PlayerSpawnManager
{

    public class SpawnManager : MonoBehaviourPunCallbacks
    {

        [Space(5)]
        [Header("Spawning Player")]
        [SerializeField]
        private GameObject playerPrefab;
        [SerializeField]
        private GameObject spawnPosition;
        [SerializeField]
        private CharacterDataScriptableObject[] charDatas;
        private int myCharacterSelection;

        private void Awake()
        {

            charDatas = Resources.LoadAll("DataSettings/CustomCharacter", typeof(CharacterDataScriptableObject)).Cast<CharacterDataScriptableObject>().ToArray();// for many data

        }

        public void SpawningPlayerNow()
        {

            myCharacterSelection = PlayerPrefs.GetInt("CharacterSelection");

            if (PlayerMovement.LocalPlayerInstance == null)
            {

                //Spawning Player
                GameObject customPlayer = PhotonNetwork.Instantiate(Path.Combine("Prefabs/Player", charDatas[myCharacterSelection].charPrefab.name), spawnPosition.transform.position, Quaternion.identity);

            }

        }

    }

}
