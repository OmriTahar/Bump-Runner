using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    public UIHandler UiHandler;
    [SerializeField] GameObject _playerPrefab;
    [SerializeField] List<GameObject> _playersSpawnPoints;

    [SerializeField] private ColorHandler _colorHandler;
    public ColorHandler colorHandler => _colorHandler;

    [SerializeField] GameObject _readyButton;
    [SerializeField] GameObject _obstaclesTilemap;
    [SerializeField] SideScroll _gridScroll;

    public int CurrentUserID;

    bool _isPlayerReady;
    bool _isPlaying = false;
    bool _isGameWon = false;
    bool _isGameLost = false;

    [SerializeField] float _slowTimeOverSeconds;
    float _currentTimeScale;


    public override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        //Time.timeScale = 0;

        if (PhotonNetwork.IsMasterClient)
        {
            _readyButton.SetActive(true);
        }
        else
        {
            _readyButton.SetActive(false);
        }

        CurrentUserID = PhotonNetwork.CurrentRoom.PlayerCount;
        CurrentUserID -= 1;

        photonView.RPC("EnteredRoom", RpcTarget.AllBuffered, CurrentUserID);
    }

    [PunRPC]
    public void EnteredRoom(int playerId)
    {
        print("RPC FUNC: Player ID: " + playerId + " has entered the room");
    }

    private void Update()
    {
        //if all players in room are ready play
        if (!_isPlaying)
        {
            if (_isPlayerReady)
            {
                Time.timeScale = 1;
                _isPlayerReady = false;
            }
        }

        if (_isGameWon)
        {
            SlowTime(true);
        }
        else if (_isGameLost)
        {
            SlowTime(false);
        }
    }
    void StartSetUP()
    {
        UiHandler.SetReadyScreen(true);
    }
    void StartGame()
    {
        _isPlayerReady = true;
        UiHandler.SetReadyScreen(false);
        //set player stuff
        var currentPlayer = PhotonNetwork.Instantiate(_playerPrefab.name, _playersSpawnPoints[CurrentUserID].transform.position, Quaternion.identity, 0);
        var ourPlayerController = currentPlayer.GetComponent<OurPlayerController>();

        if (ourPlayerController != null)
        {
            ourPlayerController.PlayerUISettings = _colorHandler.Players[CurrentUserID];
            ourPlayerController.SetPlayer(_obstaclesTilemap, CurrentUserID);
        }
        if(PhotonNetwork.IsMasterClient)
        {
            _gridScroll.StartGrid();
        }
    }

    public void PlayerIsReady()
    {
        photonView.RPC("StartGameForAll",RpcTarget.AllBuffered);
    }

    [PunRPC]
    void StartGameForAll()
    {
        Debug.Log($"Starting Game for player: {CurrentUserID}");
        StartGame();
    }

    public void GameWon()
    {
        print("Game Won!");
        _isGameWon = true;
    }

    public void GameLost()
    {
        print("Game Lost!");
        _isGameLost = true;
    }

    public void SlowTime(bool isGameWon)
    {
        _currentTimeScale = Time.timeScale;
        print("Slowing time!");

        if (_currentTimeScale <= 0.1)
        {
            print("Finished! Time scale is 0.1!");

            _currentTimeScale = 0;
            Time.timeScale = 0;

            _isPlaying = false;
            _isGameWon = false;

            UiHandler.ShowResultPanel(isGameWon);
        }
        else
        {
            _currentTimeScale -= Time.deltaTime;
            Time.timeScale = _currentTimeScale;

            print("Time Scale Decending: " + Time.timeScale);
        }
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("OnJoinedRoom was called");
    }

    

    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }

    public void GoToLobby()
    {
        SceneManager.LoadScene(0);
    }

    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    throw new System.NotImplementedException();
    //}
}
