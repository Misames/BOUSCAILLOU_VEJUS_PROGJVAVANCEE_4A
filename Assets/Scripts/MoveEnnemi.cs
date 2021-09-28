using System.Collections;
using UnityEngine;

public class MoveEnnemi : MonoBehaviour
{
    //variable joueur
    public SpriteRenderer player;

    //variable déplacement
    public float moveSpeed;
    public Rigidbody2D RigidbodyPlayer;
    private Vector3 velocity = Vector3.zero;

    //variable saut
    bool isjumping = false;
    bool isGrounding = false;
    public float jumpforce;
    public Transform GroundCheckLeft;
    public Transform GroundCheckRight;
    // variable animation
    public Animator animator;

    // variable attack

    bool isAttacking = false;
    [SerializeField]
    GameObject hitboxAttack;

    void Start()
    {
        hitboxAttack.SetActive(false);
    }

    void FixedUpdate()
    {
        isGrounding = Physics2D.OverlapArea(GroundCheckLeft.position, GroundCheckRight.position);
        float Horizontalmove = Input.GetAxis("Horizontal Joueur2") * moveSpeed * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Z) && isGrounding == true)
            isjumping = true;
        changeDirection();

        move(Horizontalmove);

        // on converti la vitesse du joueur pour qu'elle soit toujours positive
        float characterVelocity = Mathf.Abs(RigidbodyPlayer.velocity.x);
        animator.SetFloat("speed", characterVelocity);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1 joueur2") && !isAttacking)
        {
            isAttacking = true;
            animator.Play("PlayerPunch");
            StartCoroutine(Doattack());
        }

        if (Input.GetButtonDown("Fire2 Joueur2") && !isAttacking)
        {
            isAttacking = true;
            animator.Play("PlayerKick");
            StartCoroutine(Doattack());
        }

        var number = Random.Range(1, 3);
        switch (number)
        {
            case 1:
                isAttacking = true;
                animator.Play("PlayerKick");
                StartCoroutine(Doattack());
                break;
            case 2:
                isAttacking = true;
                animator.Play("PlayerPunch");
                StartCoroutine(Doattack());
                break;
            case 3: // bouger à droite
                break;
            case 4: // bouger à gauche
                break;
            case 5: // jump
                break;
            default:
                Debug.Log("erreur");
                break;
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
