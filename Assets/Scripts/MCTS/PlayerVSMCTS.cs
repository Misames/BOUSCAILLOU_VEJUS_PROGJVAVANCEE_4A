using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerVSMCTS : MonoBehaviour
{
    // Player
    public SpriteRenderer player;
    [SerializeField] PlayerMCTS secondPlayer;
    public GameObject myHealthBar;
    public int myHealth = 100;
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
        myHealthBar.GetComponent<Slider>().value = myHealth;
    }

    void FixedUpdate()
    {
        isGrounding = Physics2D.OverlapArea(GroundCheckLeft.position, GroundCheckRight.position);
        float Horizontalmove = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && isGrounding == true) isjumping = true;
        ChangeDirection();
        Move(Horizontalmove);
        // on converti la vitesse du joueur pour qu'elle soit toujours positive
        float characterVelocity = Mathf.Abs(RigidbodyPlayer.velocity.x);
        animator.SetFloat("speed", characterVelocity);
    }

    void Update()
    {
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

    void Punch(PlayerMCTS enemie)
    {
        enemie.myHealth -= 10;
        enemie.myHealthBar.GetComponent<Slider>().value = enemie.myHealth;
    }

    void Kick(PlayerMCTS enemie)
    {
        enemie.myHealth -= 20;
        enemie.myHealthBar.GetComponent<Slider>().value = enemie.myHealth;
    }
}
