using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    Rigidbody2D _rb;
    public float highJumpMutiplier = 2.5f;
    public float lowJumpMutiplier = 2f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        GameManager.OnGameStateChanged += EventSubGameStateChangeMethod;
        animator = GetComponent<Animator>();
    }
    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= EventSubGameStateChangeMethod;
    }
    private void EventSubGameStateChangeMethod(GameState state)
    {
        if (state == GameState.OVER)
        {
            Destroy(gameObject);
        }
    }
    public float speed = 6.0f;
    // Update is called once per frame
    public float jumpVelocity = 3.0f;
    public float jumpCount = 2.0f;

    private JumpState jumpState;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("ground"))
        {
            jumpState = JumpState.Grounded;
        }
    }
    void Update()
    {

        float horizontalInput = Input.GetAxis("Horizontal");

        bool isMoving = horizontalInput != 0;

        float moveHorizontal = horizontalInput * speed * Time.deltaTime;
        transform.position = new Vector2(transform.position.x + moveHorizontal, transform.position.y);
        animator.SetBool("isMoving", isMoving);

        // Physic2D Jump

        //Jump
        if (Input.GetButtonDown("Jump") && jumpCount < 2)
        {
            jumpState = JumpState.Jumping;
            _rb.velocity = Vector2.up * jumpVelocity;
            jumpCount++;
        }

        //Fall
        if (_rb.velocity.y < 0)
        {
            _rb.velocity += (highJumpMutiplier - 1) * Physics2D.gravity.y * Time.deltaTime * Vector2.up;
        }
        else if (_rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            _rb.velocity += (lowJumpMutiplier - 1) * Physics2D.gravity.y * Time.deltaTime * Vector2.up;
        }

        //Grounded
        if (jumpState == JumpState.Grounded)
        {
            jumpCount = 0;
        }
    }
    private enum JumpState
    {
        Grounded, Jumping
    }
}
