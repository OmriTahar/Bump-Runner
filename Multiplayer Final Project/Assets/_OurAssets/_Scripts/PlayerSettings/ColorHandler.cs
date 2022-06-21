using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorHandler : MonoBehaviour 
{
    [SerializeField] List<ColorButton> _buttonColors;
    public List<PlayerUISettings> Players;

    private void Start()
    {
        //set the correct player and player name
        Players[GameManager.Instance.CurrentUserID].SetPlayerSettings("Your Player");
    }

    public void SetImageColor(Color color)
    {
        Players[GameManager.Instance.CurrentUserID].SetPlayerColor(color);
    }



    public void SetPlayerImage()
    {
        //get player number (first player)
    }
}
