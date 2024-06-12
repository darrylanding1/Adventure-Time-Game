using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f;
    private bool movingRight = false; // Set to false so the enemy starts moving to the left
    private bool isGrounded;

    public Transform wallCheck; // Empty GameObject to indicate where to check for walls
    public float wallCheckDistance = 0.5f;
    public LayerMask wallLayer; // Layer mask for walls
    public Transform groundCheck; // Empty GameObject to indicate where to check for ground
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer; // Layer mask for ground

    private Animator animator;
    private Rigidbody2D rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        // Set the layer of the enemy GameObject
        gameObject.layer = LayerMask.NameToLayer("Enemy");
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded)
        {
            // Set the animator's IsIdle parameter to false when moving
            animator.SetBool("IsIdle", false);

            WallCheck();
        }
        else
        {
            // If not grounded, set IsIdle to true
            animator.SetBool("IsIdle", true);
        }

        // Move the enemy
        Move();
    }

    void Move()
    {
        if (movingRight)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
    }

    void WallCheck()
    {
        RaycastHit2D wallInfo = Physics2D.Raycast(wallCheck.position, movingRight ? Vector2.right : Vector2.left, wallCheckDistance, wallLayer);
        if (wallInfo.collider != null && wallInfo.collider.CompareTag("Wall"))
        {
            Flip();
        }
    }

    void Flip()
    {
        movingRight = !movingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    // Ignore collisions with other enemies
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Collider2D[] colliders = GetComponents<Collider2D>();
            Collider2D[] otherColliders = collision.collider.GetComponentInParent<EnemyMovement>().GetComponents<Collider2D>();

            foreach (var collider in colliders)
            {
                foreach (var otherCollider in otherColliders)
                {
                    Physics2D.IgnoreCollision(collider, otherCollider);
                }
            }
        }
    }
}
