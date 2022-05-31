using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class OurPlayerController : MonoBehaviour
{
    #region Private Fields

    [Header("General Settings")]
    [SerializeField] bool _isPlayerTwo = false;

    [Header("General Refrences")]
    [SerializeField] Rigidbody2D _rigidbody2d;
    [SerializeField] BoxCollider2D _boxCollider2D;
    [SerializeField] CapsuleCollider2D _capsuleCollider2D;

    [Header("Dash")]
    [SerializeField] float _dashPower;
    [SerializeField] float _dashCooldown;
    [SerializeField] float _currentDashCooldownRemaining;
    bool _canDash = true;

    [Header("Gravity")]
    [SerializeField] float _gravityScale;
    [SerializeField] float _gravityCooldown;

    [Header("Jump")]
    [SerializeField] float _jumpPower;
    [SerializeField] LayerMask _jumpableGround;
    bool _isGrounded;

    [Header("Animation")]
    [SerializeField] Animator _playerAnimator;

    [Header("Bumping")]
    public bool GotBumped = false;
    public bool CanGetBumped = true;

    #endregion

    #region Tilemap Collision Fields

    public Vector2 initialVelocity = new Vector2(1.0f, 10.0f);
    public GameObject tilemapGameObject;
    Tilemap _tilemap;

    #endregion

    void Start()
    {
        SetTilemapCollision();
        _rigidbody2d.gravityScale = _gravityScale;

        _boxCollider2D = GetComponent<BoxCollider2D>();
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        //if (GotBumped)
        //{
        //    CanGetBumped = false;
        //}

        if (!_isPlayerTwo)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
                TryDash();

            if (!_canDash)
                ApplyCooldown();

            _isGrounded = CheckGrounded();

            if (Input.GetKeyDown(KeyCode.Space))
                Jump();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
                TryDash();

            if (!_canDash)
                ApplyCooldown();

            _isGrounded = CheckGrounded();

            if (Input.GetKeyDown(KeyCode.UpArrow))
                Jump();
        }
        
    }

    #region PlayersCollision

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player 2")
        {
            if (!GotBumped)
            {
                print("Player 2 jumped on me!");
                StartCoroutine(JumpedOnByOtherPlayer(this));
            }
        }
        else if (collision.tag == "Player")
        {
            OurPlayerController otherGuyController = collision.GetComponent<OurPlayerController>();
            StartCoroutine(JumpedOnByOtherPlayer(otherGuyController));
        }
    }

    IEnumerator JumpedOnByOtherPlayer(OurPlayerController victim)
    {
        victim._boxCollider2D.isTrigger = true;
        Dash(-_dashPower);

        yield return new WaitForSeconds(0.15f);
        victim._boxCollider2D.isTrigger = false;
    }

    #endregion

    #region Tilemap Collision
    private void SetTilemapCollision()
    {
        _rigidbody2d.velocity = initialVelocity.x * UnityEngine.Random.Range(-1f, 1f) * Vector3.right + initialVelocity.y * Vector3.down;

        if (tilemapGameObject != null)
        {
            _tilemap = tilemapGameObject.GetComponent<Tilemap>();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 hitPosition = Vector3.zero;

        if (_tilemap != null && tilemapGameObject == collision.gameObject)
        {
            foreach (ContactPoint2D hit in collision.contacts)
            {
                hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
                hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
                _tilemap.SetTile(_tilemap.WorldToCell(hitPosition), null);
            }

            //send player backwards by 1 tile
            Debug.LogWarning("Player Hit Obstacle, need to change implementation");
            Dash(-_dashPower);
        }
    }
    #endregion

    #region Dash
    private void TryDash()
    {
        if (!_canDash)
            return;

        Dash(_dashPower);

        _canDash = false;
        _currentDashCooldownRemaining = _dashCooldown;

        _rigidbody2d.gravityScale = 0;
        StartCoroutine(DisableGravity());
    }

    private void Dash(float dashPower)
    {
        _rigidbody2d.AddForce(new Vector2(transform.position.x + dashPower, transform.position.y));
    }

    void ApplyCooldown()
    {
        _currentDashCooldownRemaining -= Time.deltaTime;

        if (_currentDashCooldownRemaining <= 0f)
        {
            _currentDashCooldownRemaining = 0;
            _canDash = true;
        }

        var cooldownPrecentage = _currentDashCooldownRemaining / _dashCooldown;
        GameManager.Instance.UiHandler.DashCooldownUI(cooldownPrecentage);
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
            return;

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
