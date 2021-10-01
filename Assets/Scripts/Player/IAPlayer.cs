using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class IAPlayer : MonoBehaviour
{
    [SerializeField] SpriteRenderer player;
    [SerializeField] PlayerVsIA secondPlayer;
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") inRange = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player") inRange = false;
    }

    void Update()
    {
        // Choisi une action de façon pseudo aléatoire
        int rand = Random.Range(1, 6);
        switch (rand)
        {
            // Coup de Poing
            case 1:
                isAttacking = true;
                animator.Play("PlayerPunch");
                StartCoroutine(DoAttack());
                if (inRange) Attack(secondPlayer, 10);
                break;
            // Coup de Pied
            case 2:
                isAttacking = true;
                animator.Play("PlayerKick");
                StartCoroutine(DoAttack());
                if (inRange) Attack(secondPlayer, 20);
                break;
            // Aller à Gauche
            case 3:
                Move(5);
                ChangeDirection();
                break;
            // Aller à Droite
            case 4:
                Move(-5);
                ChangeDirection();
                break;
            // Sauter
            case 5:
                isGrounding = Physics2D.OverlapArea(GroundCheckLeft.position, GroundCheckRight.position);
                if (isGrounding)
                {
                    animator.Play("PlayerJump");
                    RigidbodyPlayer.AddForce(new Vector2(0.0f, jumpForce));
                    isjumping = false;
                }
                break;
            default:
                break;
        }
    }

    public void Move(float horizontalMove)
    {
        Vector3 targetVelocity = new Vector2(horizontalMove, RigidbodyPlayer.velocity.y);
        RigidbodyPlayer.velocity = Vector3.SmoothDamp(RigidbodyPlayer.velocity, targetVelocity, ref velocity, 0.05f);

        if (isjumping)
        {
            animator.Play("PlayerJump");
            RigidbodyPlayer.AddForce(new Vector2(0.0f, jumpForce));
            isjumping = false;
        }
    }

    public void ChangeDirection()
    {
        if (RigidbodyPlayer.velocity.x > 0.1f) player.flipX = false;
        else if (RigidbodyPlayer.velocity.x < -0.1f) player.flipX = true;
    }

    public IEnumerator DoAttack()
    {
        yield return new WaitForSeconds(.2f);
        isAttacking = false;
    }

    void Attack(PlayerVsIA enemie, int damage)
    {
        enemie.myHealth -= damage;
        enemie.UIHealth.value = enemie.myHealth;
    }
}
