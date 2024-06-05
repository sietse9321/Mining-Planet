using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D m_Rigidbody;
    public float m_Speed = 20f;
    public float jumpForce = 10f;
    public float maxJumpTime = 0.5f; // Maximum time the jump force is applied
    private float jumpTimeCounter;
    private bool isJumping;
    private bool canJumpAgain;

    Gravity gravity;

    void Start()
    {
        // Fetch the Rigidbody from the GameObject with this script attached
        m_Rigidbody = GetComponent<Rigidbody2D>();
        gravity = GetComponent<Gravity>();
    }

    void Update()
    {
        // Check if the jump key is pressed and the character can jump again
        if (gravity.raycastHit && Input.GetKeyDown(KeyCode.Space) && canJumpAgain)
        {
            isJumping = true;
            jumpTimeCounter = maxJumpTime;
            m_Rigidbody.velocity = new Vector2(m_Rigidbody.velocity.x, jumpForce); // Initial jump force
        }

        // Check if the jump key is released
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        m_Rigidbody.AddForce(transform.right * horizontal * m_Speed, ForceMode2D.Force);

        if (isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                m_Rigidbody.velocity = new Vector2(m_Rigidbody.velocity.x, jumpForce);
                jumpTimeCounter -= Time.fixedDeltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        // Reset canJumpAgain when the character lands
        if (gravity.raycastHit && !isJumping)
        {
            canJumpAgain = true;
        }
        else
        {
            canJumpAgain = false;
        }
    }
}