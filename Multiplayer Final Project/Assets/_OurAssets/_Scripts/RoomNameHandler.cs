using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class RoomNameHandler : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _roomNamePrefab;
    [SerializeField]
    List<TextMeshProUGUI> _roomNames;

    #region Properties
    public List<TextMeshProUGUI> roomNames => _roomNames;
    #endregion

    public void AddRoomName(string roomName)
    {
        print("trying to add room: " + roomName);

        foreach (var name in _roomNames)
        {
            if (roomName == name.name)
            {
                Debug.LogWarning("Room with the same name already exists, please insert different name");
                return;
            }
        }
        var prefabInstance = Instantiate(_roomNamePrefab, transform);
        prefabInstance.name = roomName;
        prefabInstance.text = roomName;

        _roomNames.Add(prefabInstance);
        print("Added room! Name:" + roomName);
    }
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
