using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class PlayerUISettings : MonoBehaviourPunCallbacks
{

    //[SerializeField] int _playerID;
    [SerializeField] TextMeshProUGUI _playerName;
    [SerializeField] Image _playerImage;
    public Image PlayerImage => _playerImage;

    public void ChangePlayerImageColor(Color color)
    {
        PlayerImage.color = color;
    }

    public void ChangeAvatarName(string name)
    {
        _playerName.text = name;
    }
}
