//.NetSystemCollections
using System.Collections;
using System.Collections.Generic;
//Unity Library
using UnityEngine;
using UnityEditor;

//My Namespace
using VirtualExpo.MainMenu.ConnectionManager;
using VirtualExpo.MainMenu.DebugLogManager;
using VirtualExpo.MainArea.RoomManager;
using VirtualExpo.MainArea.PlayerUICustomized;

#region LauncherEditor

#if UNITY_EDITOR
[CustomEditor(typeof(LauncherConnectionManager))]
[CanEditMultipleObjects]
public class LauncherConnectionManagerEditor : Editor
{

    //this method is called everytime the method is drawn inside unity 
    public override void OnInspectorGUI()
    {

        DrawDefaultInspector();
        EditorGUILayout.HelpBox("This Script is responsible for connecting to photon servers.", MessageType.Info);

    }

}
#endif

#endregion

#region DebugMenuEditor

#if UNITY_EDITOR
[CustomEditor(typeof(DebugMenuManager))]
[CanEditMultipleObjects]
public class DebugMenuManagerEditor : Editor
{

    //this method is called everytime the method is drawn inside unity 
    public override void OnInspectorGUI()
    {

        DrawDefaultInspector();
        EditorGUILayout.HelpBox("This Script is just for telling the player about information that player needed.", MessageType.Info);

    }

}
#endif

#endregion


#region CreateOrJoinRoomManager

#if UNITY_EDITOR
[CustomEditor(typeof(CreateAndJoinRoomManager))]
[CanEditMultipleObjects]
public class CreateAndJoinRoomManagerEditor : Editor
{

    //this method is called everytime the method is drawn inside unity 
    public override void OnInspectorGUI()
    {

        DrawDefaultInspector();
        EditorGUILayout.HelpBox("This Script is for Creating or Joining Room in Photon Network.", MessageType.Info);

    }

}
#endif

#endregion


#region RoomListManager

#if UNITY_EDITOR
[CustomEditor(typeof(RoomListManager))]
[CanEditMultipleObjects]
public class RoomListManagerEditor : Editor
{

    //this method is called everytime the method is drawn inside unity 
    public override void OnInspectorGUI()
    {

        DrawDefaultInspector();
        EditorGUILayout.HelpBox("This Script is for Listing the Room in PhotonNetwork.", MessageType.Info);

    }

}
#endif

#endregion


#region PlayerUI

#if UNITY_EDITOR
[CustomEditor(typeof(PlayerUI))]
[CanEditMultipleObjects]
public class PlayerUIEditor : Editor
{

    //this method is called everytime the method is drawn inside unity 
    public override void OnInspectorGUI()
    {

        DrawDefaultInspector();
        EditorGUILayout.HelpBox("This Script is for Player UI (for player Information).", MessageType.Info);

    }

}
#endif

#endregion