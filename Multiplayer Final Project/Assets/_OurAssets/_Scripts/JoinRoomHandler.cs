using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class JoinRoomHandler : MonoBehaviour
{
    [SerializeField]
    RoomNameHandler _roomNameHandler;
    [SerializeField]
    TMP_InputField _inputField;

    public void JoinRoomByName()
    {
        if (_roomNameHandler.roomNames.Count == 0)
        {
            print("There are 0 rooms available");
            return;
        }


        for (int i = 0; i < _roomNameHandler.roomNames.Count; i++)
        {
            if (_roomNameHandler.roomNames[i].name == _inputField.text)
            {

                string roomName = _roomNameHandler.roomNames[i].name;
                Debug.Log($"Joining Room {roomName}");
                PhotonNetwork.JoinRoom(roomName);
                //create a room if dosent exist and join it
                return;
            }
        }
        Debug.LogWarning("No Matching Room Was Found");
    }
}
