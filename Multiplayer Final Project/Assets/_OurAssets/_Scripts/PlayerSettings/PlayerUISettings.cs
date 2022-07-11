using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class PlayerUISettings : MonoBehaviourPunCallbacks
{

    [SerializeField] TextMeshProUGUI _playerName;
    [SerializeField] int _playerID;
    [SerializeField] Image _playerImage;
    public Image PlayerImage => _playerImage;

    public PhotonView MyPhotonView;
    public Onchook onchook;

    private void Start()
    {
        //ChildPhotonView = PhotonView.Get(PlayerImage.gameObject);
        MyPhotonView = GetComponent<PhotonView>();
    }

    public void SetPlayerSettings(string playerNickName)
    {
        _playerName.text = playerNickName;
    }

    //public void SetPlayerColor(Color color)
    //{
    //    Debug.Log("Player ID: " + _playerID + " is trying to change his color to ." + color.ToString());
    //    _playerImage.color = color;
    //    GameManager.Instance.colorHandler.SendColor(color);
    //}

    //[PunRPC]
    //public void RecieveColorFromOtherPlayers(Vector3 rgb)
    //{
    //    _playerImage.color = new Color(rgb.x, rgb.y, rgb.z);

    //    Debug.Log("Player ID: " + _playerID + " Changed his color to: " + _playerImage.color);
    //}

    public void ChangePlayerImageColor(Color color)
    {
        PlayerImage.color = color;
    }
}
