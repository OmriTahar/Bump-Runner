using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class RoomCreationHandler : MonoBehaviour
{
    [SerializeField]
    RoomNameHandler _roomNameHandler;
    [SerializeField]
    TMP_InputField _inputField;
    
    public void CreateRoom()
    {
        print("Trying to create room for crying out loud");

        if ( _inputField.text != "")
        {
            _roomNameHandler.AddRoomName(_inputField.text);
            PhotonNetwork.CreateRoom(_inputField.text);
            Debug.Log($"Creating Room '{_inputField.text}'");
        }
        else
        {
            Debug.LogWarning("No Name Was Inserted, please add a name and try again");
        }
    }
}
