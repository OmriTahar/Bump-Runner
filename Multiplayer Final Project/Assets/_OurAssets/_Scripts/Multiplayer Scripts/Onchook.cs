using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Onchook : MonoBehaviourPunCallbacks
{

    [SerializeField] Image _image;

    [PunRPC]
    public void ChangeColor(Vector3 rgb)
    {
        _image.color = new Color(rgb.x, rgb.y, rgb.z);
    }

}
