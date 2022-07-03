using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonLobby : MonoBehaviourPunCallbacks
{

    bool _isConnecting;
    [SerializeField] string _gameVersion = "1";
    [SerializeField] byte _maxPlayerPerRoom = 3;
    PhotonView _myPhotonView;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        _isConnecting = PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = _gameVersion;
        print("Is connecting = " + _isConnecting);
        _myPhotonView = GetComponent<PhotonView>();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Connected to master");
        PhotonNetwork.JoinLobby();
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
        _myPhotonView.RPC("RoomWasCreated",RpcTarget.AllBuffered);
        PhotonNetwork.LoadLevel(1);
    }

    [PunRPC]
    public void RoomWasCreated()
    {
        Debug.Log("Everyone should know that Room Was Created");
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
            Debug.Log("Added new room: " + roomInfo.Name);
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        print("Disconnected from server. Reason: " + cause.ToString());
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        Debug.LogWarning("Joining Room Was a failure");
    }
}
