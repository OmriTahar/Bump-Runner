using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LosingCondition : MonoBehaviourPunCallbacks
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<OurPlayerController>().enabled)
        {
            GameManager.Instance.GameLost();
        }
    }
}
