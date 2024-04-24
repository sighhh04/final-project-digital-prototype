using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBehavior : MonoBehaviour
{
    public float moveSpeed = 5f; // Movement speed of the player
    public float jumpForce = 10f; // Force applied when jumping
    public float doubleJumpForce = 7f; // Force applied when double jumping
    public Material normalMaterial; // Assign in the Inspector
    public Material doubleJumpingMaterial; // Assign in the Inspector

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool canDoubleJump;
    private Renderer rend;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rend = GetComponent<Renderer>();
        rend.material = normalMaterial; // Set the initial material to normal material
    }

    void Update()
    {


        // Movement
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Jumping
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            canDoubleJump = true;
        }
        else if (!isGrounded && canDoubleJump && Input.GetKeyDown(KeyCode.Space))
        {

            rb.velocity = new Vector2(rb.velocity.x, doubleJumpForce);
            canDoubleJump = false;
            rend.material = doubleJumpingMaterial; // Change the material to double jumping material
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
            rend.material = normalMaterial; // Change the material back to normal material
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = false;
        }
    }
}
