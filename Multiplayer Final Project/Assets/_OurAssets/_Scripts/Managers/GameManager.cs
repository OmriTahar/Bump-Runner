using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    public UIHandler UiHandler;

    bool _isPlayerReady;
    bool _isPlaying = false;
    bool _isGameWon = false;
    bool _isGameLost = false;

    [SerializeField] float _slowTimeOverSeconds;
    float _currentTimeScale;


    void Start()
    {
        Time.timeScale = 0;
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

    void StartGame()
    {
        UiHandler.SetReadyScreen(true);
    }

    public void PlayerIsReady()
    {
        _isPlayerReady = true;
        UiHandler.SetReadyScreen(false);
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

    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }
}
