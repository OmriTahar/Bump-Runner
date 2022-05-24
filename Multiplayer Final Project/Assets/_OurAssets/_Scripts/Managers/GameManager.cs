using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{

    public UIHandler UiHandler;

    bool _isPlayerReady;
    bool _isPlaying = false;

    [SerializeField] float _slowTimeOverSeconds;


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
            }
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
        _isPlaying = false;
        StartCoroutine(SlowTimeTillStop());
    }

    IEnumerator SlowTimeTillStop()
    {
        //while (Time.timeScale > 0)
        //{
        //    Time.timeScale -= _slowTimeOverSeconds * Time.deltaTime;
        //}
        yield return null;
    }
}
