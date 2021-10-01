using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class SecondPlayer : MonoBehaviour
{
    [SerializeField] SpriteRenderer player;
    [SerializeField] Player secondPlayer;
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
        // Coup de Poing
        if (Input.GetButtonDown("Fire1 joueur2") && !isAttacking)
        {
            isAttacking = true;
            animator.Play("PlayerPunch");
            StartCoroutine(DoAttack());
            if (inRange) Attack(secondPlayer, 10);
        }

        // Coup de Pied
        if (Input.GetButtonDown("Fire2 Joueur2") && !isAttacking)
        {
            isAttacking = true;
            animator.Play("PlayerKick");
            StartCoroutine(DoAttack());
            if (inRange) Attack(secondPlayer, 10);
        }
    }

    // Permet de mouvoir le Player
    // Horizontalement et verticalement avec le saut
    void Move(float _Horizontalmove)
    {
        Vector3 targetVelocity = new Vector2(_Horizontalmove, RigidbodyPlayer.velocity.y);
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
    void Attack(Player enemie, int damage)
    {
        enemie.myHealth -= damage;
        enemie.UIHealth.value = enemie.myHealth;
    }
}
