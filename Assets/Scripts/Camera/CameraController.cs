using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float currentY = 0;

    // Update is called once per frame
    void Update()
    {
        if(player.position.y >- 0) {currentY = player.position.y;} else {currentY = 0;}

        transform.position = new Vector3(player.position.x, currentY, transform.position.z);
    }
}
