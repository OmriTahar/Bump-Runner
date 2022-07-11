using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColorHandler : MonoBehaviourPunCallbacks
{

    [SerializeField] List<ColorButton> _buttonColors;
    public List<PlayerUISettings> Players;

    string _myNickname;

    private void Start()
    {
        _myNickname = PhotonNetwork.NickName;
        Players[GameManager.Instance.CurrentUserID].SetPlayerSettings(PhotonNetwork.NickName); //set the correct player and player name
    }

    public void SetImageColor(Color color)
    {
        SendColor(color, GameManager.Instance.CurrentUserID);
    }

    public void SendColor(Color color, int id)
    {
        photonView.RPC("ChangeColor", RpcTarget.AllBuffered, new Vector3(color.r, color.g, color.b), id);
    }

    [PunRPC]
    public void ChangeColor(Vector3 rgb, int id)
    {
        var color = new Color(rgb.x, rgb.y, rgb.z);

        print("Player id: " + id + " is changing color.");

        Players[id].ChangePlayerImageColor(color);
    }
}
