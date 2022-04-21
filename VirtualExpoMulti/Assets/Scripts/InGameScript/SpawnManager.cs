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


        public void SpawningPlayerNow()
        {

            //Spawning Player
            GameObject customPlayer = PhotonNetwork.Instantiate(Path.Combine("Prefabs", this.playerPrefab.name), spawnPosition.transform.position, Quaternion.identity);
            customPlayer.name = customPlayer.GetPhotonView().Owner.NickName + " Player";

        }

    }

}
