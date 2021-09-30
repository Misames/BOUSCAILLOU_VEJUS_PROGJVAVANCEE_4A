using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class IAPlayer : MonoBehaviour
{
    // Player
    public SpriteRenderer player;
    public PlayerVsIA secondPlayer;
    public int myHealth = 100;
    public Slider UIHealth;
    bool inRange;

    // Move
    public float moveSpeed;
    public Rigidbody2D RigidbodyPlayer;
    Vector3 velocity = Vector3.zero;

    // Jump
    bool isjumping = false;
    bool isGrounding = false;
    public float jumpforce;
    [SerializeField] Transform GroundCheckLeft;
    [SerializeField] Transform GroundCheckRight;

    // Animation
    public Animator animator;

    // Attack
    bool isAttacking = false;
    [SerializeField] GameObject hitboxAttack;

    // Event
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") inRange = true;

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player") inRange = false;
    }

    void Start()
    {
        hitboxAttack.SetActive(false);
        UIHealth.value = myHealth;
    }

    void Update()
    {
        int rand = Random.Range(1, 6);

        switch (rand)
        {
            case 1:
                StartCoroutine(waitAttack());
                isAttacking = true;
                animator.Play("PlayerPunch");
                StartCoroutine(DoAttack());
                if (inRange) Punch(secondPlayer);
                break;
            case 2:
                StartCoroutine(waitAttack());
                isAttacking = true;
                animator.Play("PlayerKick");
                StartCoroutine(DoAttack());
                if (inRange) Kick(secondPlayer);
                break;
            case 3:
                Move(5);
                ChangeDirection();
                break;
            case 4:
                Move(-5);
                ChangeDirection();
                break;
            case 5:
                isGrounding = Physics2D.OverlapArea(GroundCheckLeft.position, GroundCheckRight.position);
                if (isGrounding)
                {
                    animator.Play("PlayerJump");
                    RigidbodyPlayer.AddForce(new Vector2(0.0f, jumpforce));
                    isjumping = false;
                }
                break;
            default:
                break;
        }

        if (Input.GetButtonDown("Fire1") && !isAttacking)
        {
            isAttacking = true;
            animator.Play("PlayerPunch");
            StartCoroutine(DoAttack());
            if (inRange) Punch(secondPlayer);
        }

        if (Input.GetButtonDown("Fire2") && !isAttacking)
        {
            isAttacking = true;
            animator.Play("PlayerKick");
            StartCoroutine(DoAttack());
            if (inRange) Kick(secondPlayer);
        }
    }

    void Move(float _Horizontalmove)
    {
        Vector3 targetVelocity = new Vector2(_Horizontalmove, RigidbodyPlayer.velocity.y);
        RigidbodyPlayer.velocity = Vector3.SmoothDamp(RigidbodyPlayer.velocity, targetVelocity, ref velocity, 0.05f);

        if (isjumping)
        {
            animator.Play("PlayerJump");
            RigidbodyPlayer.AddForce(new Vector2(0.0f, jumpforce));
            isjumping = false;
        }
    }

    void ChangeDirection()
    {
        if (RigidbodyPlayer.velocity.x > 0.1f) player.flipX = false;
        else if (RigidbodyPlayer.velocity.x < -0.1f) player.flipX = true;
    }

    IEnumerator DoAttack()
    {
        hitboxAttack.SetActive(true);
        yield return new WaitForSeconds(.2f);
        hitboxAttack.SetActive(false);
        isAttacking = false;
    }

    IEnumerator waitAttack()
    {
        yield return new WaitForSeconds(5f);
    }

    void Punch(PlayerVsIA enemie)
    {
        enemie.myHealth -= 10;
        enemie.UIHealth.value = enemie.myHealth;
    }

    void Kick(PlayerVsIA enemie)
    {
        enemie.myHealth -= 20;
        enemie.UIHealth.value = enemie.myHealth;
    }
}
