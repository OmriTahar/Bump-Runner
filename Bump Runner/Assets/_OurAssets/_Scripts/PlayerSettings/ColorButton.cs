using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ColorButton : MonoBehaviour
{
    [SerializeField] private UnityEvent<Color> _onSetImageColor;
    public Image colorImage;

    private void Start()
    {
        colorImage = GetComponent<Image>();
    }

    public void SendColor()
    {
        _onSetImageColor.Invoke(colorImage.color);
    }
}
