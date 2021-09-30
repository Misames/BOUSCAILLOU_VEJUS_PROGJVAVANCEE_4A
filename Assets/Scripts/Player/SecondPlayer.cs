using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class SecondPlayer : MonoBehaviour
{
    // Player
    [SerializeField] Player secondPlayer;
    public SpriteRenderer player;
    public Slider UIHealth;
    public byte myHealth = 100;
    bool inRange;

    // Move
    public float moveSpeed;
    public Rigidbody2D RigidbodyPlayer;
    private Vector3 velocity = Vector3.zero;

    // Jump
    bool isjumping = false;
    bool isGrounding = false;
    public float jumpforce;
    public Transform GroundCheckLeft;
    public Transform GroundCheckRight;

    // Animation
    public Animator animator;

    // variable attack
    [SerializeField] GameObject hitboxAttack;
    bool isAttacking = false;

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
    }

    void FixedUpdate()
    {
        isGrounding = Physics2D.OverlapArea(GroundCheckLeft.position, GroundCheckRight.position);
        float Horizontalmove = Input.GetAxis("Horizontal Joueur2") * moveSpeed * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Z) && isGrounding == true) isjumping = true;
        ChangeDirection();
        Move(Horizontalmove);
        // on converti la vitesse du joueur pour qu'elle soit toujours positive
        float characterVelocity = Mathf.Abs(RigidbodyPlayer.velocity.x);
        animator.SetFloat("speed", characterVelocity);
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1 joueur2") && !isAttacking)
        {
            isAttacking = true;
            animator.Play("PlayerPunch");
            StartCoroutine(DoAttack());
            if (inRange) Punch(secondPlayer);
        }

        if (Input.GetButtonDown("Fire2 Joueur2") && !isAttacking)
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

    void Punch(Player enemie)
    {
        enemie.myHealth -= 10;
        enemie.UIHealth.value = enemie.myHealth;
    }

    void Kick(Player enemie)
    {
        enemie.myHealth -= 20;
        enemie.UIHealth.value = enemie.myHealth;
    }
}
