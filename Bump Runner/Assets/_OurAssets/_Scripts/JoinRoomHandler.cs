using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class JoinRoomHandler : MonoBehaviour
{

    [SerializeField] private RoomNameHandler _roomNameHandler;
    [SerializeField] private TMP_InputField _inputField;

    public void JoinRoomByName()
    {
        PhotonNetwork.JoinRoom(_inputField.text);
    }
}
