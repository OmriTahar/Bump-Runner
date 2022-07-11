using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScroll : MonoBehaviour
{
    [SerializeField]
    float _scrollSpeed = 0.1f;
    public bool canMove = false;
    public void Update()
    {
        if (canMove)
        {
            Move();
        }
    }

    private void Move()
    {
        transform.Translate(Vector2.left*Time.deltaTime*_scrollSpeed);
    }
}
