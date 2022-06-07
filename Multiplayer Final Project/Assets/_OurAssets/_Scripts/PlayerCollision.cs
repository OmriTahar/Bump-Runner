using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] OurPlayerController _player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Debug.Log("Hit On Head");
            //I need to go backwards
            //_player.BoostPlayer();
            //other need to go forwards (send over network)
        }
    }
}
