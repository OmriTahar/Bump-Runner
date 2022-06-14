using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonLobby : MonoBehaviourPunCallbacks
{
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = "0.5.1";
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Connected to master");

        PhotonNetwork.JoinLobby();
        //getListOf Avilable Rooms;
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        print("Disconnected from server. Reason: " + cause.ToString());
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("Joined Lobby");
    }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        Debug.Log("Room Was Created");
        PhotonNetwork.LoadLevel(1);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        Debug.LogWarning("Join Room Failed!!!");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);
        foreach (var roomInfo in roomList)
        {
            Debug.Log(roomInfo.Name);
        }
    }
}
