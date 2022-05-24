using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class OurPlayerController : MonoBehaviour
{
    #region Private Fields
    [SerializeField]
    Rigidbody2D _rigidbody2d;

    //dash
    bool _canDash = true;
    [Header("Dash Settings")]
    [SerializeField]
    float _dashPower;
    [SerializeField] float _dashCooldown;
    [SerializeField] float _currentDashCooldownRemaining;

    [Header("Gravity Settings")]
    [SerializeField]
    float _gravityScale;
    [SerializeField]
    float _gravityCooldown;

    //jump
    [SerializeField]
    float _jumpPower;
    bool _isGrounded;

    [SerializeField]
    BoxCollider2D _boxCollider2D;
    [SerializeField] LayerMask _jumpableGround;

    //Animations
    [SerializeField]
    Animator _playerAnimator;
    #endregion

    #region Tilemap Collision Fields
    public Vector2 initialVelocity = new Vector2(1.0f, 10.0f);
    public GameObject tilemapGameObject;
    #endregion

    Tilemap tilemap;
    void Start()
    {
        SetTilemapCollision();
        _rigidbody2d.gravityScale = _gravityScale;
    }

    #region Tilemap Collision
    private void SetTilemapCollision()
    {
        _rigidbody2d.velocity = initialVelocity.x * UnityEngine.Random.Range(-1f, 1f) * Vector3.right + initialVelocity.y * Vector3.down;
        if (tilemapGameObject != null)
        {
            tilemap = tilemapGameObject.GetComponent<Tilemap>();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 hitPosition = Vector3.zero;
        if (tilemap != null && tilemapGameObject == collision.gameObject)
        {
            foreach (ContactPoint2D hit in collision.contacts)
            {
                hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
                hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
                tilemap.SetTile(tilemap.WorldToCell(hitPosition), null);
            }
            //send player backwards by 1 tile
            Debug.LogWarning("Player Hit Obstacle, need to change implementation");
        Dash(-_dashPower);
        }
    }
    #endregion

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            TryDash();
        }

        if (!_canDash)
            ApplyCooldown();

        _isGrounded = CheckGrounded();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Is grounded:" + _isGrounded);
            Jump();
        }
    }

    #region Dash
    private void TryDash()
    {
        if (!_canDash)
        {
            Debug.Log("Can Not Dash");
            return;
        }

        Debug.Log("trying to Dash");
        Dash(_dashPower);

        _canDash = false;
        _currentDashCooldownRemaining = _dashCooldown;

        _rigidbody2d.gravityScale = 0;

        //StartCoroutine(DashCooldown());
        StartCoroutine(DisableGravity());
    }

    private void Dash(float dashPower)
    {
        _rigidbody2d.AddForce(new Vector2(transform.position.x + dashPower, transform.position.y));
    }

    IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(_dashCooldown);
        _canDash = true;
    }

    void ApplyCooldown()
    {
        _currentDashCooldownRemaining -= Time.deltaTime;

        if (_currentDashCooldownRemaining <= 0f)
        {
            _currentDashCooldownRemaining = 0;
            _canDash = true;
            
        }
    }

    IEnumerator DisableGravity()
    {
        yield return new WaitForSeconds(_gravityCooldown);
        _rigidbody2d.gravityScale = _gravityScale;
    }
    #endregion
    #region Jump
    private void Jump()
    {
        if (!_isGrounded)
        {
            return;
        }
        _rigidbody2d.AddForce(new Vector2(0, _jumpPower), ForceMode2D.Impulse);


    }
    private bool CheckGrounded()
    {
        var isGrounded = Physics2D.BoxCast(_boxCollider2D.bounds.center, _boxCollider2D.bounds.size, 0f, Vector2.down, .1f, _jumpableGround);
        if (isGrounded)
        {
            _playerAnimator.SetBool("IsJumping", false);
        }
        else
        {
            _playerAnimator.SetBool("IsJumping", true);
        }
        return isGrounded;
    }
    #endregion
}
