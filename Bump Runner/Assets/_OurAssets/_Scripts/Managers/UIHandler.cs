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
    [SerializeField] TextMeshProUGUI _winningText;
    [SerializeField] TextMeshProUGUI _losingText;

    [Header("Player UI")]
    [SerializeField] Image _dashCooldownImage_P1;

    public void Start()
    {
        if (PhotonNetwork.CurrentRoom != null)
        {
            _roomName.text = "Room Name: " + PhotonNetwork.CurrentRoom.Name;
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
        if (_dashCooldownImage_P1 != null)
            _dashCooldownImage_P1.fillAmount = 1 - cooldownAmount;
    }

    public void SetDashColor(Color color)
    {
        _dashCooldownImage_P1.color = color;
    }

    public void ShowResultPanel(bool isGameWon) // Before Changes
    {
        _resultPanel.SetActive(true);
        
        if (isGameWon)
            _winningText.gameObject.SetActive(true);
        else
            _losingText.gameObject.SetActive(true);
    }

    public void ChangeResultsText(string name, int winPlacement)
    {
        switch (winPlacement)
        {
            case 1:
                _winningText.text = "First Place: " + name + "\n";
                break;
            case 2:
                _winningText.text += "Second Place: " + name + "\n";
                break;
            case 3:
                _winningText.text += "Third Place: " + name;
                break;
            default:
                break;
        }
    }
}
