using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] SpriteRenderer player;
    [SerializeField] SecondPlayer secondPlayer;
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D RigidbodyPlayer;
    [SerializeField] Transform GroundCheckLeft;
    [SerializeField] Transform GroundCheckRight;
    [SerializeField] float moveSpeed = 300f;
    [SerializeField] float jumpForce = 500f;
    Vector3 velocity = Vector3.zero;
    bool isjumping, isGrounding, isAttacking, inRange = false;
    public int myHealth = 100;
    public Slider UIHealth;

    // Permet de déterminer si no Player est à porter pour attaquer
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") inRange = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player") inRange = false;
    }

    void FixedUpdate()
    {
        isGrounding = Physics2D.OverlapArea(GroundCheckLeft.position, GroundCheckRight.position);
        float horizontalMove = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounding == true) isjumping = true;

        ChangeDirection();
        Move(horizontalMove);

        // On converti la vitesse du joueur pour qu'elle soit toujours positive
        float characterVelocity = Mathf.Abs(RigidbodyPlayer.velocity.x);
        animator.SetFloat("speed", characterVelocity);
    }

    void Update()
    {
        // Coup de Poing
        if (Input.GetButtonDown("Fire1") && !isAttacking)
        {
            isAttacking = true;
            animator.Play("PlayerPunch");
            StartCoroutine(DoAttack());
            if (inRange) Attack(secondPlayer, 10);
        }

        // Coup de Pied
        if (Input.GetButtonDown("Fire2") && !isAttacking)
        {
            isAttacking = true;
            animator.Play("PlayerKick");
            StartCoroutine(DoAttack());
            if (inRange) Attack(secondPlayer, 20);
        }
    }

    // Permet de mouvoir le Player
    // Horizontalement et verticalement avec le saut
    void Move(float horizontalMove)
    {
        Vector3 targetVelocity = new Vector2(horizontalMove, RigidbodyPlayer.velocity.y);
        RigidbodyPlayer.velocity = Vector3.SmoothDamp(RigidbodyPlayer.velocity, targetVelocity, ref velocity, 0.05f);

        // Saut
        if (isjumping)
        {
            animator.Play("PlayerJump");
            RigidbodyPlayer.AddForce(new Vector2(0.0f, jumpForce));
            isjumping = false;
        }
    }

    // Change l'orientation du Player
    void ChangeDirection()
    {
        if (RigidbodyPlayer.velocity.x > 0.1f) player.flipX = false;
        else if (RigidbodyPlayer.velocity.x < -0.1f) player.flipX = true;
    }

    IEnumerator DoAttack()
    {
        yield return new WaitForSeconds(.2f);
        isAttacking = false;
    }

    // Attaquer un enemie et lui retire des PV
    void Attack(SecondPlayer enemie, int damage)
    {
        enemie.myHealth -= damage;
        enemie.UIHealth.value = enemie.myHealth;
    }

}
