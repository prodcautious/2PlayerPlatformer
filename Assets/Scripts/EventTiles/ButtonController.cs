using UnityEngine;
using System.Collections;

public class ButtonController : MonoBehaviour
{
    public GameObject target;
    public ButtonColor buttonColor;

    public float pressDepth = 0.1f; // How far down the button moves
    public float lerpSpeed = 10f;   // How fast the button moves
    
    [Header("Timer Settings")]
    public bool useTimer = false;    // Toggle to enable timer functionality
    public float timerDuration = 3f; // Duration in seconds before door closes after stepping off
    
    private Vector3 originalPosition;
    private Vector3 pressedPosition;
    private Coroutine timerCoroutine;

    public enum ButtonColor
    {
        Red,
        Blue
    }

    private bool isPressed = false;
    private int currentPressCount = 0;

    private void Start()
    {
        originalPosition = transform.position;
        pressedPosition = originalPosition - new Vector3(0, pressDepth, 0);
    }

    private void Update()
    {
        Vector3 targetPosition = isPressed ? pressedPosition : originalPosition;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * lerpSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool validBlueCollision = (buttonColor == ButtonColor.Blue) &&
            (collision.CompareTag("Blue") || collision.CompareTag("Box"));

        bool validRedCollision = (buttonColor == ButtonColor.Red) &&
            (collision.CompareTag("Red") || collision.CompareTag("Box"));

        if (validBlueCollision || validRedCollision)
        {
            currentPressCount++;

            if (!isPressed)
            {
                isPressed = true;
                target.SetActive(false);
            }
            
            // If there's an active timer and something steps on the button again,
            // stop the timer to prevent the door from closing
            if (useTimer && timerCoroutine != null)
            {
                StopCoroutine(timerCoroutine);
                timerCoroutine = null;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        bool validBlueCollision = (buttonColor == ButtonColor.Blue) &&
            (collision.CompareTag("Blue") || collision.CompareTag("Box"));

        bool validRedCollision = (buttonColor == ButtonColor.Red) &&
            (collision.CompareTag("Red") || collision.CompareTag("Box"));

        if (validBlueCollision || validRedCollision)
        {
            currentPressCount--;

            if (currentPressCount <= 0)
            {
                currentPressCount = 0;
                
                if (useTimer)
                {
                    // Start timer before closing the door
                    if (timerCoroutine != null)
                    {
                        StopCoroutine(timerCoroutine);
                    }
                    timerCoroutine = StartCoroutine(ButtonTimerCoroutine());
                }
                else
                {
                    // Original behavior - close immediately
                    isPressed = false;
                    target.SetActive(true);
                }
            }
        }
    }
    
    private IEnumerator ButtonTimerCoroutine()
    {
        // Keep the button visually pressed and door open during timer
        yield return new WaitForSeconds(timerDuration);
        
        // After timer completes, release button and close door
        isPressed = false;
        target.SetActive(true);
        timerCoroutine = null;
    }
    
    private void OnDisable()
    {
        // Safety cleanup in case the object is disabled with an active timer
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }
    }
}