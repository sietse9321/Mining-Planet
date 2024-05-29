using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    Rigidbody2D rigidBody;
    public float gravity;
    float velocity;
    float airTime;
    float raycastlenght = 0.2f;
    public RaycastHit2D hit;
    public RaycastHit2D hitLeft;
    public RaycastHit2D hitRight;
    LayerMask level;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        level = LayerMask.GetMask("Level");
    }

    // Update is called once per frame
    void Update()
    {
        hit = Physics2D.Raycast(new Vector2(transform.position.x + 0.05f, transform.localPosition.y), -Vector2.up, raycastlenght, level);
        hitLeft = Physics2D.Raycast(new Vector2(transform.position.x - 0.05f, transform.localPosition.y), -Vector2.up, raycastlenght, level);
        hitRight = Physics2D.Raycast(transform.position, -Vector2.up, raycastlenght, level);
        Debug.DrawRay(new Vector3(transform.localPosition.x + 0.05f, transform.localPosition.y, 0.0f), -Vector2.up * raycastlenght, Color.red);
        Debug.DrawRay(new Vector3(transform.localPosition.x - 0.05f, transform.localPosition.y, 0.0f), -Vector2.up * raycastlenght, Color.red);
        Debug.DrawRay(transform.position, -Vector2.up * raycastlenght, Color.red);
        if (hit || hitLeft || hitRight)
        {
            Debug.DrawRay(transform.position, -Vector2.up * raycastlenght, Color.green);
            gravity = 30;
        }
                
        
    }

    void FixedUpdate()
    {

        rigidBody.AddForce(-transform.up * gravity);
        //Faster the longer he falls for
        //normal gravity when walking
    }
}
