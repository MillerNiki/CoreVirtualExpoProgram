//.NetSystemCollections
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

//Unity Library
using UnityEngine;
using VirtualExpo.MainArea.PlayerUICustomized;

//PhotonPunLibrarySDK
using Photon.Pun;
using Photon.Realtime;

using VirtualExpo.CharacterDatas;


namespace VirtualExpo.Player.AvatarSelection
{

    public class AvatarSelection : MonoBehaviourPunCallbacks
    {

        //[Header("character selection")]
        //private PhotonView PV;
        //[SerializeField]
        //private CharacterDataScriptableObject[] charDatas;
        //private int myCharacterSelection;
        //private GameObject charSelection;

        private void Awake()
        {

            /*charDatas = Resources.LoadAll("DataSettings/CustomCharacter", typeof(CharacterDataScriptableObject)).Cast<CharacterDataScriptableObject>().ToArray();// for many data
            PV = this.GetComponent<PhotonView>();

            myCharacterSelection = PlayerPrefs.GetInt("CharacterSelection");

            if (PV.IsMine)
            {

                PV.RPC("SelectionOfCharacter", RpcTarget.AllBuffered, myCharacterSelection);
                //SelectionOfCharacter(myCharacterSelection);

            }*/

        }


        #region CharacterSelection

        /*[PunRPC]
        void SelectionOfCharacter(int index)
        {

            //instantiate the model
            this.charSelection = Instantiate(charDatas[index].charPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            //this.charSelection = PhotonNetwork.Instantiate(Path.Combine("Distant Lands/Prefabs", charDatas[index].charPrefab.name), new Vector3(0, 0, 0), Quaternion.identity);
            this.charSelection.transform.SetParent(this.transform);

        }*/

        #endregion

    }

}


