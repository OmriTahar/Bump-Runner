using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class ColorHandler : MonoBehaviourPunCallbacks
{

    public PhotonView MyPhotonView;

    [SerializeField] List<ColorButton> _buttonColors;
    public List<PlayerUISettings> Players;

    private void Awake()
    {
        MyPhotonView = GetComponent<PhotonView>();
    }

    private void Start()
    {
        //set the correct player and player name
        Players[GameManager.Instance.CurrentUserID].SetPlayerSettings("Your Player");
    }

    public void SetImageColor(Color color)
    {
        Players[GameManager.Instance.CurrentUserID].SetPlayerColor(color);
    }

    [PunRPC]
    public void SendColor(Color color)
    {
        MyPhotonView.RPC("ChangeColor", RpcTarget.AllBuffered, new Vector3(color.r, color.g, color.b));
    }

    [PunRPC]
    public void ChangeColor(Vector3 rgb)
    {
        var color = new Color(rgb.x, rgb.y, rgb.z);
        print("RPC 'change color' was called.");

        SetColor(color);
    }

    private void SetColor(Color color)
    {
        print("Color print: " + color.ToString());
    }
}
