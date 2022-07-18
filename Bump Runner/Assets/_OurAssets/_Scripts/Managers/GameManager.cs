using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using Photon.Realtime;

public class GameManager : MonoSingleton<GameManager>
{
    #region Variables

    [Header("Player Refrences")]
    [SerializeField] GameObject _playerPrefab;
    [SerializeField] List<GameObject> _playersSpawnPoints;
    [SerializeField] List<GameObject> _playerAvatars;

    [Header("Other Refrences")]
    [SerializeField] PlayerUI_Handler _playerUI_Handler;
    public PlayerUI_Handler playerUI_Handler => _playerUI_Handler;
    public UIHandler UiHandler;
    [SerializeField] GameObject _readyButton;
    [SerializeField] GameObject _obstaclesTilemap;
    [SerializeField] SideScroll _gridScroll;

    [Header("Info")]
    public int CurrentUserID;

    Tilemap _tilemap;
    public Tilemap tilemap { set => _tilemap = value; }

    private bool _isPlayerReady;
    private bool _isPlaying = false;
    private bool _isGameWon = false;
    private bool _isGameLost = false;
    private float _currentTimeScale;
    private string _myName = PhotonNetwork.NickName;

    // --------------- RESULTS ----------
    private List <int> _winOrder = new List<int>();
    private int _maxPlayersThatCanWin = 3;
    private bool _isGameOver = false;
    [SerializeField] private float _gameOverCooldown = 0.5f;

    #endregion

    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            _readyButton.SetActive(true);
        }
        else
            _readyButton.SetActive(false);

        CurrentUserID = PhotonNetwork.CurrentRoom.PlayerCount;
        CurrentUserID -= 1;

        _playerUI_Handler.SetPlayerName(_myName);
        photonView.RPC("EnteredRoom", RpcTarget.AllBuffered, CurrentUserID);
    }

    private void Update()
    {
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
        if (_winOrder.Count == _maxPlayersThatCanWin)
        {

            Debug.Log("Game is over");
            StartCoroutine(GameOverCooldown());
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
        _maxPlayersThatCanWin = PhotonNetwork.CurrentRoom.PlayerCount;

        var currentPlayer = PhotonNetwork.Instantiate(_playerPrefab.name, _playersSpawnPoints[CurrentUserID].transform.position, Quaternion.identity, 0);
        var ourPlayerController = currentPlayer.GetComponent<OurPlayerController>();

        if (ourPlayerController != null)
        {
            ourPlayerController.PlayerUISettings = _playerUI_Handler.Players[CurrentUserID];
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
        _isGameWon = true;
        photonView.RPC("AddWinningPlayer", RpcTarget.AllBuffered, CurrentUserID);
        photonView.RPC("SendWinningPlayer", RpcTarget.AllBuffered, _myName);
    }

    [PunRPC]
    public void AddWinningPlayer(int playerID)
    {
        _winOrder.Add(playerID);
        Debug.Log($"Player {playerID} has reached the vicroty gate!");
    }

    [PunRPC]
    public void SendWinningPlayer(string playerName)
    {
        UiHandler.ChangeResultsText(playerName, _winOrder.Count);
    }

    public void GameLost()
    {
        _isGameLost = true;
        photonView.RPC("ReduceMaxWinsAmount", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void ReduceMaxWinsAmount()
    {
        _maxPlayersThatCanWin -= 1;
    }

    private void GameOver()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.AutomaticallySyncScene = false;
            photonView.RPC("LeaveGameRoom", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    public void LeaveGameRoom()
    {
            Time.timeScale = 1;
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene(0);
    }

    IEnumerator GameOverCooldown()
    {
        yield return new WaitForSeconds(_gameOverCooldown);
        GameOver();
    }

    public void SlowTime(bool isGameWon)
    {
        _currentTimeScale = Time.timeScale;

        if (_currentTimeScale <= 0.1)
        {
            _currentTimeScale = 0;
            Time.timeScale = 0.1f;

            UiHandler.ShowResultPanel(isGameWon);
        }
        else
        {
            _currentTimeScale -= Time.deltaTime;
            Time.timeScale = _currentTimeScale;
        }
    }

    [PunRPC]
    public void EnteredRoom(int playerId)
    {
        print("Player ID: " + playerId + " has entered the room");
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        _playerUI_Handler.SetPlayerName(_myName);

        photonView.RPC("TogglePlayerAvatar", RpcTarget.AllBuffered, CurrentUserID);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        Debug.Log("Player nubmer: " + otherPlayer.ActorNumber + " has DISCONNECTED!");
        photonView.RPC("TogglePlayerAvatar", RpcTarget.AllBuffered, otherPlayer.ActorNumber - 1);
    }

    public void SendTileDestruction(Vector3 hitPosition)
    {
        photonView.RPC("DestroyTile", RpcTarget.AllBuffered, hitPosition);
    }

    [PunRPC]
    public void DestroyTile(Vector3 hitPosition)
    {
        _tilemap.SetTile(_tilemap.WorldToCell(hitPosition), null);
    }

    [PunRPC]
    public void TogglePlayerAvatar(int currentUserID)
    {
        if (_playerAvatars[currentUserID].activeInHierarchy)
        {
            _playerAvatars[currentUserID].SetActive(false);
        }
        else
            _playerAvatars[currentUserID].SetActive(true);
    }
}
