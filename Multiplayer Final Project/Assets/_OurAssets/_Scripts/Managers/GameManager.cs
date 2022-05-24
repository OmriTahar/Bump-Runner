using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{

    public UIHandler UiHandler;

    bool _isPlayerReady;
    bool _isPlaying = false;
    bool _isGameWon = false;

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
            SlowTime();
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

    public void Victory()
    {
        /*
        reach victory gate (Win effect - particles)
        start slow time method
        when time.scale = 0 - stop all players
        declare winner
        */

        print("Victory!");
        _isGameWon = true;
        _currentTimeScale = Time.timeScale;
    }

    public void SlowTime()
    {
        print("Slow time!");

        if (_currentTimeScale <= 0.1)
        {
            print("Finished! Time scale is 0.1!");

            _currentTimeScale = 0;
            Time.timeScale = 0;

            _isPlaying = false;
            _isGameWon = false;

            UiHandler.ShowVictoryPanel();
        }
        else
        {
            _currentTimeScale -= Time.deltaTime;
            Time.timeScale = _currentTimeScale;

            print("Time Scale: " + Time.timeScale);
        }
    }

}
