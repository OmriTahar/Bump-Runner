using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class RoomNameHandler : MonoBehaviourPunCallbacks
{

    [SerializeField] private TextMeshProUGUI _roomNamePrefab;
    [SerializeField] private List<TextMeshProUGUI> _roomNames;
    public List<TextMeshProUGUI> RoomNames => _roomNames;

    public void AddRoomName(string roomName)
    {
        print("Trying to add room: " + roomName);

        foreach (var name in _roomNames)
        {
            if (roomName == name.name)
            {
                Debug.LogWarning("Room with the same name already exists, please insert different name");
                return;
            }
        }

        CreateRoomName(roomName);
    }

    private void CreateRoomName(string roomName)
    {
        var prefabInstance = Instantiate(_roomNamePrefab, transform);
        prefabInstance.name = roomName;
        prefabInstance.text = roomName;
        _roomNames.Add(prefabInstance);

        print("Added room! room name: " + roomName);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);

        foreach (var roomInfo in roomList)
        {
            Debug.Log("Added new room: " + roomInfo.Name);
            CreateRoomName(roomInfo.Name);
        }
    }

    //public void RefreshRoomList()
    //{
    //    photon
    //}

    public void RemoveRoomName(string roomName)
    {
        for (int i = 0; i < _roomNames.Count; i++)
        {
            if (_roomNames[i].name == roomName)
            {
                _roomNames.RemoveAt(i);
                return;
            }
        }
    }

}
