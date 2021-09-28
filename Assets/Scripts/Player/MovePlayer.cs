using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    //variable joueur
    public SpriteRenderer player;

    //variable déplacement
    public float moveSpeed;
    public Rigidbody2D RigidbodyPlayer;
    private Vector3 velocity = Vector3.zero;

    //variable saut
    private bool isjumping = false;
    private bool isGrounding = false;
    public float jumpforce;
    public Transform GroundCheckLeft;
    public Transform GroundCheckRight;
    // variable animation
    public Animator animator;

    void FixedUpdate()
    {
        isGrounding = Physics2D.OverlapArea(GroundCheckLeft.position, GroundCheckRight.position);
        float Horizontalmove = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && isGrounding == true)
            isjumping = true;
        changeDirection();
        move(Horizontalmove);

        // on converti la vitesse du joueur pour qu'elle soit toujours positive
        float characterVelocity = Mathf.Abs(RigidbodyPlayer.velocity.x);
        animator.SetFloat("speed", characterVelocity);
    }

    void move(float _Horizontalmove)
    {
        Vector3 targetVelocity = new Vector2(_Horizontalmove, RigidbodyPlayer.velocity.y);
        RigidbodyPlayer.velocity = Vector3.SmoothDamp(RigidbodyPlayer.velocity, targetVelocity, ref velocity, 0.05f);

        if (isjumping == true)
        {
            RigidbodyPlayer.AddForce(new Vector2(0.0f, jumpforce));
            isjumping = false;
        }
    }

    void changeDirection()
    {
        if (RigidbodyPlayer.velocity.x > 0.1f)
            player.flipX = false;
        else if (RigidbodyPlayer.velocity.x < -0.1f)
            player.flipX = true;
    }

}
