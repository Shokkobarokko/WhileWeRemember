using NUnit.Framework;
using UnityEditor.Callbacks;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 7f;
    private Rigidbody2D rb;
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
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
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
            transform.localScale = new Vector3(1,1,1);
        } else if (horizontal < 0)
        {
            transform.localScale = new Vector3(-1,1,1);
        }
    }
}
