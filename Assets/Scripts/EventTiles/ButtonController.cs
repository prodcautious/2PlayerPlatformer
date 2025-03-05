using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public GameObject target;
    public ButtonColor buttonColor;
    
    public enum ButtonColor
    {
        Red,
        Blue
    }

    private bool isPressed = false;
    private int currentPressCount = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collision is valid for this button color
        bool validBlueCollision = (buttonColor == ButtonColor.Blue) && 
            (collision.CompareTag("PlayerOne") || collision.CompareTag("Box"));
        
        bool validRedCollision = (buttonColor == ButtonColor.Red) && 
            (collision.CompareTag("PlayerTwo") || collision.CompareTag("Box"));

        // Increment press count and deactivate target if not already pressed
        if (validBlueCollision || validRedCollision)
        {
            currentPressCount++;
            
            if (!isPressed)
            {
                isPressed = true;
                target.SetActive(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Check if the collision is valid for this button color
        bool validBlueCollision = (buttonColor == ButtonColor.Blue) && 
            (collision.CompareTag("PlayerOne") || collision.CompareTag("Box"));
        
        bool validRedCollision = (buttonColor == ButtonColor.Red) && 
            (collision.CompareTag("PlayerTwo") || collision.CompareTag("Box"));

        // Decrement press count
        if (validBlueCollision || validRedCollision)
        {
            currentPressCount--;

            // Reactivate target only when no objects are pressing the button
            if (currentPressCount <= 0)
            {
                isPressed = false;
                target.SetActive(true);
                currentPressCount = 0;
            }
        }
    }
}