using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public string acceptedPlayerTag; // "Red" or "Blue"
    public bool isPlayerOnDoor { get; private set; }
    
    // Optional visual feedback components
    public SpriteRenderer doorRenderer;
    public Color activatedColor = Color.yellow;
    private Color originalColor;
    
    private void Start()
    {
        isPlayerOnDoor = false;
        
        if (doorRenderer != null)
        {
            originalColor = doorRenderer.color;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(acceptedPlayerTag))
        {
            isPlayerOnDoor = true;
            
            // Visual feedback when player enters
            if (doorRenderer != null)
            {
                doorRenderer.color = Color.Lerp(originalColor, activatedColor, 0.5f);
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(acceptedPlayerTag))
        {
            isPlayerOnDoor = false;
            
            // Reset visual feedback
            if (doorRenderer != null)
            {
                doorRenderer.color = originalColor;
            }
        }
    }
    
    public void ActivateDoor()
    {
        // Full visual feedback when both players are on doors
        if (doorRenderer != null)
        {
            doorRenderer.color = activatedColor;
        }
        
        // You could add particle effects or animations here
    }
}