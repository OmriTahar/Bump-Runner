using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColorHandler : MonoBehaviourPunCallbacks
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

    public void ChangePlayerImageColor(Color color)
    {
        PlayerImage.color = color;
    }
}
