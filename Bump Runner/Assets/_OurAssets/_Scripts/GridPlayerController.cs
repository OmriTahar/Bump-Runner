using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPlayerController : MonoBehaviour
{

    public float MoveSpeed = 5f;
    public Transform MovePoint;

    public AudioClip jumpAudio;
    public AudioClip respawnAudio;
    public AudioClip ouchAudio;

    //public JumpState jumpState = JumpState.Grounded;
    private bool stopJump;

    public Collider2D collider2d;
    public AudioSource audioSource;

    public bool controlEnabled = true;

    bool jump;
    SpriteRenderer spriteRenderer;
    internal Animator animator;

    public Bounds Bounds => collider2d.bounds;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        collider2d = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        NewGridMovement();
    }

    private void NewGridMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position, MovePoint.position, MoveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, MovePoint.position) <= 0.05f)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                MovePoint.position = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
            }
        }
    }
}
