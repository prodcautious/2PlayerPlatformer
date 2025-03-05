using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerOne;
    public Transform playerTwo;
    public float currentY;
    
    public float minY = 0;
    public float followSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the camera position to the exact average between players
        // regardless of minimum Y value
        Vector3 initialMidpoint = (playerOne.position + playerTwo.position) / 2f;
        currentY = initialMidpoint.y; // No minimum check on start
        
        // Set initial camera position
        Vector3 startPosition = new Vector3(initialMidpoint.x, currentY, transform.position.z);
        transform.position = startPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 midpoint = (playerOne.position + playerTwo.position) / 2f;
        
        // No minimum check - always follow the players
        currentY = midpoint.y;
        
        Vector3 targetPosition = new Vector3(midpoint.x, currentY, transform.position.z);
        transform.position = targetPosition;
    }
}