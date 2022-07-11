using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI_Handler : MonoBehaviourPunCallbacks
{

    [SerializeField] List<ColorButton> _buttonColors;
    public List<PlayerUISettings> Players;

    public void SetImageColor(Color color)
    {
        photonView.RPC("ChangeColor", RpcTarget.AllBuffered, new Vector3(color.r, color.g, color.b), GameManager.Instance.CurrentUserID);
    }

    [PunRPC]
    public void ChangeColor(Vector3 rgb, int id)
    {
        var color = new Color(rgb.x, rgb.y, rgb.z);
        Players[id].ChangePlayerImageColor(color);
    }

    public void SetPlayerName(string name)
    {
        photonView.RPC("ChangeName", RpcTarget.AllBuffered, GameManager.Instance.CurrentUserID, name);
    }

    [PunRPC]
    public void ChangeName(int id, string name)
    {
        Players[id].ChangeAvatarName(name);
    }
}
