using NUnit.Framework;
using UnityEditor.Callbacks;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 7f;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private float horizontal;
    //Точка возле ног робота
    [SerializeField] private Transform groundCheck;
    // Радиус области проверки
    [SerializeField] private float groundCheckRadius = 0.15f;
    //Какие слои считаются землёй
    [SerializeField] private LayerMask groundLayer;
    //Находится ли робот на земле
    private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        animator.SetBool("IsMoving", horizontal != 0);
        Debug.Log("Horizontal: " + horizontal);
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position, 
            groundCheckRadius, 
            groundLayer);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

    
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * moveSpeed, rb.linearVelocity.y);
        if (horizontal > 0)
        {
            spriteRenderer.flipX = false;
        } else if (horizontal < 0)
        {
            spriteRenderer.flipX = true;
        }
    }
}
