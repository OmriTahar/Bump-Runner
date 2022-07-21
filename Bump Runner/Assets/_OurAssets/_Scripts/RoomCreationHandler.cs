using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class RoomCreationHandler : MonoBehaviourPunCallbacks
{

    [SerializeField] private RoomNameHandler _roomNameHandler;
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private TextMeshProUGUI _errorText;

    public void CreateRoom()
    {
        if ( _inputField.text != "")
        {
            _roomNameHandler.AddRoomName(_inputField.text);
            PhotonNetwork.CreateRoom(_inputField.text);
            Debug.Log($"Creating Room '{_inputField.text}'");
        }
        else
        {
            _errorText.text = "Room name cannot be empty";
            Debug.LogWarning("No Name Was Inserted, please add a name and try again");
        }
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        _errorText.text = "Room named '" + _inputField.text + "' is already exists";
    }
}
