using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMCTS : MonoBehaviour
{
   // Player
    public SpriteRenderer playerMCTSSprite;
    public PlayerMCTS Playermcts;
    public PlayerVSMCTS ennemi;
    public GameObject myHealthBar;
    public int myHealth = 100;
    [HideInInspector]
    public bool inRange;

    // Move
    [HideInInspector]
    public float moveSpeed;
    public Rigidbody2D RigidbodyPlayer;
    Vector3 velocity = Vector3.zero;

    // Jump
    [HideInInspector]
    public bool isjumping = false;
    [HideInInspector]
    public bool isGrounding = false;
    [HideInInspector]
    public float jumpforce;
    [SerializeField] public Transform GroundCheckLeft;
    [SerializeField] public Transform GroundCheckRight;

    // Animation
    public Animator animator;

    // Attack
    [HideInInspector]
    public bool isAttacking = false;
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
                if (inRange) Punch(ennemi);
                break;
            case 2:
                StartCoroutine(waitAttack());
                isAttacking = true;
                animator.Play("PlayerKick");
                StartCoroutine(DoAttack());
                if (inRange) Kick(ennemi);
                break;
            case 3:
                Move(5);
                ChangeDirection();
                break;
            case 4:
                Move(-5);
                ChangeDirection();
                break;
            case 5 :
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
            if (inRange) Punch(ennemi);
        }

        if (Input.GetButtonDown("Fire2") && !isAttacking)
        {
            isAttacking = true;
            animator.Play("PlayerKick");
            StartCoroutine(DoAttack());
            if (inRange) Kick(ennemi);
        }
    }

   public void Move(float _Horizontalmove)
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

    public void ChangeDirection()
    {
        if (RigidbodyPlayer.velocity.x > 0.1f) playerMCTSSprite.flipX = false;
        else if (RigidbodyPlayer.velocity.x < -0.1f) playerMCTSSprite.flipX = true;
    }

    public IEnumerator DoAttack()
    {
        hitboxAttack.SetActive(true);
        yield return new WaitForSeconds(.2f);
        hitboxAttack.SetActive(false);
        isAttacking = false;
    }

    public IEnumerator waitAttack()
    {
        yield return new WaitForSeconds(5f);
    }

   public void Punch(PlayerVSMCTS enemie)
    {
        enemie.myHealth -= 10;
        enemie.myHealthBar.GetComponent<Slider>().value = enemie.myHealth;
    }

   public void Kick(PlayerVSMCTS enemie)
    {
        enemie.myHealth -= 20;
        enemie.myHealthBar.GetComponent<Slider>().value = enemie.myHealth;
    } 
  
}
