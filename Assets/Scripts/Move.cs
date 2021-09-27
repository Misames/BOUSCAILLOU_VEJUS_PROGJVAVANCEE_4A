using UnityEngine;

public class Move : MonoBehaviour
{

    public float speed;
    public float jumpForce;
    public Rigidbody2D rb;
    public Transform groundCheckLeft;
    public Transform groundCheckRight;
    Vector3 velocity = Vector3.zero;
    public bool isJumping;
    public bool isGrounded;

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);

        var horizontalMove = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        if (Input.GetButtonDown("Jump") && isGrounded)
            isJumping = true;

        MoveMethod(horizontalMove);
    }

    void MoveMethod(float horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2(horizontalMovement, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);
        if (isJumping == true)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            isJumping = false;
        }
    }
}
