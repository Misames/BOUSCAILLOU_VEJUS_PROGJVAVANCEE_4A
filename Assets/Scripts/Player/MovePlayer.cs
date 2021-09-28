using System.Collections;
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

    // variable attack
    private bool isAttacking = false;
    [SerializeField] GameObject hitboxAttack;

    void Start()
    {
        hitboxAttack.SetActive(false);
    }

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

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && !isAttacking)
        {
            isAttacking = true;
            animator.Play("PlayerPunch");
            StartCoroutine(Doattack());
        }

        if (Input.GetButtonDown("Fire2") && !isAttacking)
        {
            isAttacking = true;
            animator.Play("PlayerKick");
            StartCoroutine(Doattack());
        }
    }

    void move(float _Horizontalmove)
    {
        Vector3 targetVelocity = new Vector2(_Horizontalmove, RigidbodyPlayer.velocity.y);
        RigidbodyPlayer.velocity = Vector3.SmoothDamp(RigidbodyPlayer.velocity, targetVelocity, ref velocity, 0.05f);

        if (isjumping == true)
        {
            animator.Play("PlayerJump");
            RigidbodyPlayer.AddForce(new Vector2(0.0f, jumpforce));
            isjumping = false;
        }
    }

    void changeDirection()
    {
        if (RigidbodyPlayer.velocity.x > 0.1f) player.flipX = false;
        else if (RigidbodyPlayer.velocity.x < -0.1f) player.flipX = true;
    }

    IEnumerator Doattack()
    {
        hitboxAttack.SetActive(true);
        yield return new WaitForSeconds(.2f);
        hitboxAttack.SetActive(false);
        isAttacking = false;
    }

}
