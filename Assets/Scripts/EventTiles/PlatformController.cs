using UnityEngine;
using System.Collections.Generic;

public class PlatformController : MonoBehaviour
{
    public Transform pointA;       // Starting position
    public Transform pointB;       // Ending position
    public float moveSpeed = 2f;   // Movement speed

    private Vector3 positionA;
    private Vector3 positionB;
    private Vector3 targetPosition;
    private Vector3 previousPosition;
    private List<Transform> ridingObjects = new List<Transform>();

    private void Start()
    {
        // Store the world positions at start
        positionA = pointA.position;
        positionB = pointB.position;
        
        // Start the platform at the midpoint between pointA and pointB
        transform.position = (positionA + positionB) / 2f;
        previousPosition = transform.position;
        targetPosition = positionB;  // Set the first target to pointB
    }

    private void Update()
    {
        previousPosition = transform.position;
        
        // Move the platform towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Once the platform reaches its target, switch to the other point
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            targetPosition = (targetPosition == positionA) ? positionB : positionA;
        }
    }
    
    private void LateUpdate()
    {
        // Move any objects riding the platform
        Vector3 moveDelta = transform.position - previousPosition;
        
        foreach (Transform rider in ridingObjects)
        {
            if (rider != null)
            {
                rider.position += moveDelta;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If contact is from above, add them to riders
        if (IsContactPointFromAbove(collision))
        {
            if (!ridingObjects.Contains(collision.transform))
            {
                ridingObjects.Add(collision.transform);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Remove from riders list when they leave
        if (ridingObjects.Contains(collision.transform))
        {
            ridingObjects.Remove(collision.transform);
        }
    }
    
    private bool IsContactPointFromAbove(Collision2D collision)
    {
        // Check if the contact is from above (player is on top of platform)
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y < -0.5f)  // Normal points downward from platform
            {
                return true;
            }
        }
        return false;
    }
}