using UnityEngine;
using UnityEngine.SceneManagement;

// Attach this to a manager object in your scene
public class DoorController : MonoBehaviour
{
    public Door redDoor;
    public Door blueDoor;
    public string nextLevelName;
    public float delayBeforeLoading = 1.0f;
    
    private bool isChangingLevel = false;

    private void Update()
    {
        // Only check if both doors are triggered when we're not already changing level
        if (!isChangingLevel && redDoor.isPlayerOnDoor && blueDoor.isPlayerOnDoor)
        {
            StartLevelChange();
        }
    }
    
    private void StartLevelChange()
    {
        isChangingLevel = true;
        Debug.Log("Both players on doors! Changing level...");
        
        // Optional visual feedback
        redDoor.ActivateDoor();
        blueDoor.ActivateDoor();
        
        // Change level after short delay
        Invoke("LoadNextLevel", delayBeforeLoading);
    }
    
    private void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevelName);
    }
}