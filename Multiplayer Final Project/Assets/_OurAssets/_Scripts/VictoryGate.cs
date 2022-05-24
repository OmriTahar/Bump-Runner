using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryGate : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameManager.Instance.Victory();
        }
    }
}
