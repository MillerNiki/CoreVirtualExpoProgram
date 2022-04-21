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

namespace VirtualExpo.MainArea.RoomManager
{

    /// Color Note : 
    /// - Skyblue : Connection Granted (#87ceeb)
    /// - NeonGreen : Joined room / Joined to master (#39ff14)
    /// - Orange : left room / Disconnected (#FFA500)
    /// - NeonViolet : Joining Room Scene (#B026FF)
    /// - NeonYellow : Ping Connection (#FFF01F)
    /// - Snow : RoomUpdate

    public class RoomListManager : MonoBehaviourPunCallbacks
    {

        #region Private Variable

        internal List<RoomInfo> roomListCached = new List<RoomInfo>();

        #endregion

        #region Photon Pun Callbacks Method(s)


        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {

            if (roomList.Count == 0)
            {


                Debug.LogWarning("<color=red>Room is empty.</color>");

            }
            else
            {
                
                Debug.Log("<color=snow>Room has been found. Room Count : " + PhotonNetwork.CountOfRooms + "</color>");

                foreach (RoomInfo info in roomList)
                {

                    if (info.RemovedFromList)
                    {

                        //Lambda expression : get the index which ever listing that has the same room name
                        int index = roomListCached.FindIndex(x => x.Name == info.Name);
                        //if index isn't -1 (found)
                        if (index != -1)
                        {

                            roomListCached.RemoveAt(index);
                            Debug.Log(info.Name + " room info has been removed from list");

                        }

                    }
                    else
                    {

                        //Lambda expression : get the index which ever listing that has the same room name
                        int index = roomListCached.FindIndex(x => x.Name == info.Name);
                        //if index is -1 (nothing found)
                        if (index == -1)
                        {


                            roomListCached.Add(info);
                            Debug.Log(info.Name + " room info has been added to list");

                        }
                        else
                        {
                            //modify listing here
                            roomListCached[index] = info;
                            Debug.Log(info.Name + " room info has been Updated");
                        }

                    }

                }

            }

            

        }

        public override void OnJoinedRoom()
        {
            roomListCached.Clear();
        }

        #endregion

    }

}