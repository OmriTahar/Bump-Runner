using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryGate : MonoBehaviour
{
    [Header("Effects")]
    [SerializeField] ParticleSystem _winningParticles;
    bool _isWon = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            var winningPlayer = collision.GetComponent<OurPlayerController>();
            if (winningPlayer != null && winningPlayer.enabled && !_isWon)
            {
                _isWon = true;
                _winningParticles.Play();
                GameManager.Instance.GameWon();
            }
        }
    }
}
