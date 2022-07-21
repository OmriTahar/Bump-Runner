using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class JoinRoomHandler : MonoBehaviourPunCallbacks
{

    [SerializeField] private RoomNameHandler _roomNameHandler;
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private TextMeshProUGUI _errorText;

    public void JoinRoomByName()
    {
        PhotonNetwork.JoinRoom(_inputField.text);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        _errorText.text = "Room named '" + _inputField.text + "' does not exists";
    }
}
