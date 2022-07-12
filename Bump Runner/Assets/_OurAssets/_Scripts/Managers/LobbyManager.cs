using Photon.Pun;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{

    [SerializeField] GameObject _introCanvas;
    [SerializeField] GameObject _lobbyCanvas;

    [SerializeField] TMP_InputField _inputField;


    private void Awake()
    {
        if (PhotonNetwork.NickName == "")
        {
            _introCanvas.SetActive(true);
            _lobbyCanvas.SetActive(false);
        }
        else
        {
            GoToLobby();
        }
    }

    public void GoToLobby()
    {
        _introCanvas.SetActive(false);
        _lobbyCanvas.SetActive(true);
    }

    public void SetNickName()
    {
        if (_inputField.text == "")
            _inputField.text = "Eggplant";

        PhotonNetwork.NickName = _inputField.text;
    }
}
