using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUISettings : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI _playerName;

    [SerializeField] Image _playerImage;
    public Image PlayerImage => _playerImage;

    [SerializeField] int _playerID;


    public void SetPlayerSettings(string playerNickName)
    {
        _playerName.text = playerNickName;
    }
    public void SetPlayerColor(Color color)
    {
        _playerImage.color = color;
    }
}
