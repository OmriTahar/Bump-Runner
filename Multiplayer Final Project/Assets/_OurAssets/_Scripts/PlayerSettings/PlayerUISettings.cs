using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class PlayerUISettings : MonoBehaviourPunCallbacks, IPunObservable
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

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(_playerImage.color);
        }
        else
        {
            _playerImage.color = (Color)stream.ReceiveNext();
        }
    }
}
