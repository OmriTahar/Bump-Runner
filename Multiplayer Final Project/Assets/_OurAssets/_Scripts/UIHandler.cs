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
    [SerializeField]
    GameObject _readyScreen;
    public void Start()
    {
        _roomName.text = "RoomName: " + PhotonNetwork.CurrentRoom.Name;
    }
    public void SetReadyScreen(bool toActivate)
    {
        _readyScreen.SetActive(toActivate);
    }
}
