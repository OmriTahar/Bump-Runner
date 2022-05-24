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

    [Header("Panels")]
    [SerializeField] GameObject _readyPanel;

    [Header("Game Results")]
    [SerializeField] GameObject _resultPanel;
    [SerializeField] GameObject _winningText;
    [SerializeField] GameObject _losingText;

    [Header("Player UI")]
    [SerializeField] Image _dashCooldownImage;

    public void Start()
    {
        if (PhotonNetwork.CurrentRoom != null)
        {
            _roomName.text = "RoomName: " + PhotonNetwork.CurrentRoom.Name;
        }

        _readyPanel.SetActive(true);
        _resultPanel.SetActive(false);
    }

    public void SetReadyScreen(bool toActivate)
    {
        _readyPanel.SetActive(toActivate);
    }

    public void DashCooldownUI(float cooldownAmount)
    {
        if (_dashCooldownImage != null)
        {
            _dashCooldownImage.fillAmount = 1 - cooldownAmount;
        }
    }

    public void ShowResultPanel(bool isGameWon)
    {
        _resultPanel.SetActive(true);
        
        if (isGameWon)
            _winningText.SetActive(true);
        else
            _losingText.SetActive(true);
    }
}
