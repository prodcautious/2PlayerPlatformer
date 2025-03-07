using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public Transform pointA;       // Starting position
    public Transform pointB;       // Ending position
    public float moveSpeed = 2f;   // Movement speed
    public PlatformColor platformColor;

    private Vector3 targetPosition;
    private Collider2D platformCollider;

    public enum PlatformColor
    {
        Red,
        Blue
    }

    private void Start()
    {
        // Start the platform at the midpoint between pointA and pointB
        transform.position = (pointA.position + pointB.position) / 2f;
        targetPosition = pointB.position;  // Set the first target to pointB
        platformCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        // Move the platform towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Once the platform reaches its target, switch to the other point
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            targetPosition = (targetPosition == pointA.position) ? pointB.position : pointA.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleInvalidPlayer(collision.collider);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        HandleInvalidPlayer(collision.collider);
    }

    private void HandleInvalidPlayer(Collider2D playerCollider)
    {
        if (platformColor == PlatformColor.Red && playerCollider.CompareTag("Blue"))
        {
            Physics2D.IgnoreCollision(playerCollider, platformCollider, true);
        }
        else if (platformColor == PlatformColor.Blue && playerCollider.CompareTag("Red"))
        {
            Physics2D.IgnoreCollision(playerCollider, platformCollider, true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Re-enable collision when they fully leave the platform
        if (collision.collider.CompareTag("Blue") || collision.collider.CompareTag("Red"))
        {
            Physics2D.IgnoreCollision(collision.collider, platformCollider, false);
        }
    }
}
