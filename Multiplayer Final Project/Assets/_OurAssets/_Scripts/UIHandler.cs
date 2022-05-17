using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class UIHandler : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _roomName;
    public void Start()
    {
        _roomName.text = "RoomName: " + PhotonNetwork.CurrentRoom.Name;
    }
}
