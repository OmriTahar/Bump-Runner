using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryGate : MonoBehaviour
{
    [Header("Effects")]
    [SerializeField] ParticleSystem _winningParticles;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _winningParticles.Play();
            GameManager.Instance.GameWon();
        }
    }
}
