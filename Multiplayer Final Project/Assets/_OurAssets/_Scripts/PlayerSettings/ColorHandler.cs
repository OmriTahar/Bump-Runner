using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorHandler : MonoBehaviour
{
    [SerializeField] Image _playerImage;
    [SerializeField] List<ColorButton> _buttonColors;
    private void Start()
    {
        SetButtonsID();
        //listen to color event;
    }
    private void SetButtonsID()
    {
        for (int i = 0; i < _buttonColors.Count; i++)
        {
            _buttonColors[i].buttonID = i;
        }
    }
    public void SetImageColor(int buttonID)
    {
        _playerImage.color = _buttonColors[buttonID].colorImage.color;
        //set actual player color
        //do not let others set their color
    }
}
