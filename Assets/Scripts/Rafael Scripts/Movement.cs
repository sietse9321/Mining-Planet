using UnityEngine;
using System.Collections.Generic;

public class Movement : MonoBehaviour
{
    Rigidbody2D m_Rigidbody;
    public float m_Speed = 20f;
    public float jumpAmount = 49f;


    Gravity gravity;

    void Start()
    {
        // Fetch the Rigidbody from the GameObject with this script attached
        m_Rigidbody = GetComponent<Rigidbody2D>();
        gravity = GetComponent<Gravity>();
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); 
        
        m_Rigidbody.AddForce(transform.right * horizontal * m_Speed, ForceMode2D.Force);
        
        if(gravity.hit && Input.GetKey(KeyCode.Space) || gravity.hitRight && Input.GetKey(KeyCode.Space) || gravity.hitLeft && Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Jump");
            m_Rigidbody.AddForce(transform.up * jumpAmount, ForceMode2D.Impulse);
            // 27 is 1 tile long jump
            // 40 is 2 tile long jump
        }



    }
}