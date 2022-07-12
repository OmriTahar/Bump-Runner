using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LosingCondition : MonoBehaviourPunCallbacks
{
    bool hasLost = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var losingPlayer = collision.GetComponent<OurPlayerController>();

            if (losingPlayer != null && losingPlayer.enabled && !hasLost)
            {
                hasLost = true;
                GameManager.Instance.GameLost();
            }
            else
                Debug.LogWarning("Losing player is null");
        }
    }
}
