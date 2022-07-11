using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{

    [SerializeField] GameObject _introCanvas;
    [SerializeField] GameObject _lobbyCanvas;

    private void Awake()
    {
        _introCanvas.SetActive(true);
        _lobbyCanvas.SetActive(false);
    }

    public void GoToLobby()
    {
        _introCanvas.SetActive(false);
        _lobbyCanvas.SetActive(true);
    }
}
