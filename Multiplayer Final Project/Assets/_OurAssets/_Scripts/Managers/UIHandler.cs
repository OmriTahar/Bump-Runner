using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class UIHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _roomName;

    [SerializeField] GameObject _readyScreen;

    [Header("Player UI")]
    [SerializeField] Image _dashCooldownImage;

    public void Start()
    {
        _roomName.text = "RoomName: " + PhotonNetwork.CurrentRoom.Name;
    }

    public void SetReadyScreen(bool toActivate)
    {
        _readyScreen.SetActive(toActivate);
    }

    public void DashCooldownUI(float cooldownAmount)
    {
        if (_dashCooldownImage != null)
        {
            _dashCooldownImage.fillAmount = cooldownAmount;
        }
    }
}
