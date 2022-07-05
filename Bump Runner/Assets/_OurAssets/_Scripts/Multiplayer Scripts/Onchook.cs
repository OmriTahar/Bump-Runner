using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Onchook : MonoBehaviourPunCallbacks
{

    [SerializeField] Image _image;
    public PhotonView MyPhotonView;

    private void Awake()
    {
        MyPhotonView = GetComponent<PhotonView>();
    }

    [PunRPC]
    public void ChangeColor(Vector3 rgb)
    {
        _image.color = new Color(rgb.x, rgb.y, rgb.z);
        print("onchoock change color was called.");
    }

}
