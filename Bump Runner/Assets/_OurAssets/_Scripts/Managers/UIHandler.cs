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
    [SerializeField] Image _dashCooldownImage_P1;
    [SerializeField] Image _dashCooldownImage_P2;

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

    public void DashCooldownUI(float cooldownAmount, bool isPlayerTwo)
    {
        if (!isPlayerTwo)
        {
            if (_dashCooldownImage_P1 != null)
                _dashCooldownImage_P1.fillAmount = 1 - cooldownAmount;
        }
        else
        {
            if (_dashCooldownImage_P2 != null)
                _dashCooldownImage_P2.fillAmount = 1 - cooldownAmount;
        }
    }
    public void SetDashColor(Color color)
    {
        _dashCooldownImage_P1.color = color;
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
