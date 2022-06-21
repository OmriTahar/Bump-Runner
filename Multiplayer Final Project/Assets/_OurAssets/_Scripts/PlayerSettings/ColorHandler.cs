using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorHandler : MonoBehaviour
{
    [SerializeField] List<ColorButton> _buttonColors;
    public List<PlayerUISettings> Players;
    int currentPlayerID = 0;

    private void Start()
    {
        //set the correct player and player name
        Players[currentPlayerID].SetPlayerSettings("Your Player");
    }

    public void SetImageColor(Color color)
    {
        Players[currentPlayerID].SetPlayerColor(color);
    }
    public void SetPlayerImage()
    {
        //get player number (first player)
    }
}
