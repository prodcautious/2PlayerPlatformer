using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerOneController : MonoBehaviour
{

    Rigidbody2D rb;
    float move = 0;
    float speed = 5;
    bool jump = false;
    float jumpForce = 10.0f;
    Transform feet;
    Vector2 feetbox;
    LayerMask groundMask;
    Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        feet = transform.Find("Feet");
        feetbox = new Vector2(0.8f, 0.1f);
        groundMask = LayerMask.GetMask("Ground");
        startPos = this.gameObject.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {

        var grounded = Physics2D.OverlapBox(feet.position, feetbox, 0, groundMask);
        move = 0;

        if (Input.GetKey(KeyCode.A)) move = -1;
        if (Input.GetKey(KeyCode.D)) move = 1;
        if (Input.GetKeyDown(KeyCode.W) && grounded) jump = true;

        if(this.gameObject.transform.position.y < -9)
        {
            this.gameObject.transform.position = startPos;
        }
        
    }

    void FixedUpdate()
    {
        if (move != 0)
        {
            var velocity = rb.velocity;
            velocity.x = move * speed;
            rb.velocity = velocity;
        }

        if (jump)
        {
            jump = false;
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
