using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ColorButton : MonoBehaviour
{
    [SerializeField] private UnityEvent<Color> setImageColor;
    public Image colorImage;

    private void Start()
    {
        colorImage = GetComponent<Image>();
    }
    //send color through event
    public void SendColor()
    {
        setImageColor.Invoke(colorImage.color);
    }
}
