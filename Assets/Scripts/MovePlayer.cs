using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D RigidbodyPlayer;
    private Vector3 velocity = Vector3.zero;
    private bool isjumping = false;
    private bool isGrounding = false;
    public float jumpforce;
    public Transform GroundCheckLeft;
    public Transform GroundCheckRight;
    void FixedUpdate()
    {
        isGrounding = Physics2D.OverlapArea(GroundCheckLeft.position, GroundCheckRight.position);
        
        float Horizontalmove = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounding == true)
        {
            isjumping = true;
        }
        move(Horizontalmove);
    }

    void move(float _Horizontalmove)
    {
        Vector3 targetVelocity = new Vector2(_Horizontalmove, RigidbodyPlayer.velocity.y);
        RigidbodyPlayer.velocity = Vector3.SmoothDamp(RigidbodyPlayer.velocity, targetVelocity, ref velocity, 0.05f);

        if (isjumping == true)
        {
            
            RigidbodyPlayer.AddForce(new Vector2(0.0f,jumpforce));
            isjumping = false;
        }
    }
    
    
}
