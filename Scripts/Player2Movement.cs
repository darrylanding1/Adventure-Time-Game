using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player2Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool isGrounded;
    private float Speed; // Changed to Speed with capital S
    private bool canJump = true;
    private Camera mainCamera;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        mainCamera = Camera.main;

        if (animator == null)
        {
            Debug.LogError("Animator component not found on Player 2.");
        }
    }

    private void FixedUpdate()
    {
        // Check if the player is grounded
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius);
        isGrounded = false;
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Ground") || collider.CompareTag("Player"))
            {
                isGrounded = true;
                break;
            }
        }
        Debug.Log($"Player2 is grounded: {isGrounded}");
    }

    private void Update()
    {
        // Player 2 movement with arrow keys
        float moveHorizontal = Input.GetAxis("Horizontal2");
        Speed = Mathf.Abs(moveHorizontal); // Assign to Speed with capital S
        rb.velocity = new Vector2(moveHorizontal * moveSpeed, rb.velocity.y);
        ClampPosition();
        FlipPlayer(moveHorizontal);

        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded && canJump)
        {
            Debug.Log("Player2 Jumping");
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            StartCoroutine(JumpCooldown());
        }

        // Update animator parameters
        if (animator != null)
        {
            animator.SetFloat("Speed", Speed); // Change to Speed with capital S
            animator.SetBool("IsGrounded", isGrounded);
        }
    }

    private void FlipPlayer(float horizontalMovement)
    {
        // Flip the player sprite based on the direction of movement
        if (horizontalMovement > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (horizontalMovement < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    private IEnumerator JumpCooldown()
    {
        canJump = false;
        yield return new WaitForSeconds(0.5f); // Adjust the cooldown duration as needed
        canJump = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Finish"))
        {
            SceneManager.LoadScene("Finish");
        }
    }

    private void ClampPosition()
    {
        Vector3 pos = transform.position;
        Vector3 viewPos = mainCamera.WorldToViewportPoint(pos);

        viewPos.x = Mathf.Clamp(viewPos.x, 0.05f, 0.95f);
        viewPos.y = Mathf.Clamp(viewPos.y, 0.05f, 0.95f);

        transform.position = mainCamera.ViewportToWorldPoint(viewPos);
    }
}
