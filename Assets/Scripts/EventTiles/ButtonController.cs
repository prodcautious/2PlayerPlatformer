using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public GameObject target;
    public ButtonColor buttonColor;

    public float pressDepth = 0.1f; // How far down the button moves
    public float lerpSpeed = 10f;   // How fast the button moves

    private Vector3 originalPosition;
    private Vector3 pressedPosition;

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
                isPressed = false;
                target.SetActive(true);
                currentPressCount = 0;
            }
        }
    }
}
