using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManagerMCTS : MonoBehaviour
{
    private Player player1;
    PlayerMCTS playerMCTS;
    public Vector2 player1pos;
    public Vector2 player2pos;
    public int lifeJ1;
    public int lifeJ2;
    float time, timeDuration;
    private int bestActions = -1;
    private int totalResult;
    private float totalpourcentage;
    private int nbTotal;
    private int precedenteAction;
    private MoveLeftMCTS _moveLeftMcts;
    private MoveRightMCTS _moveRightMcts;
    private JumpMCTS _jumpMcts;
    private KickMCTS _kickMcts;
    private PunchMCTS _punchMcts;
    private bool endSimu = false;
    private int compteur = 0;
    public IAPlayer _IaPlayer;
    private void Start()
    {
        //init des joueurs virtuel
        lifeJ1 = 100;
        lifeJ2 = 100;
        var transform1 = player1.transform;
        player1pos.x = transform1.position.x;
        player1pos.y = transform1.position.y;
        player2pos.x = 20;
        player2pos.y = 0;
    }

    private void Update()
    {
        ChoixActionSimu();
        if (endSimu = true)
        {
            
        }
    }

    void ChoixAction()
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
                _IaPlayer.Move(5);
                _IaPlayer.ChangeDirection();
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
        }
    }
    void ChoixActionSimu()
    {
        if (lifeJ2 != 0 && compteur != 20)
        {
             if (precedenteAction != 5)
                   {
                       int rand = Random.Range(1, 6);
           
                       switch (rand)
                       {
                           case 1 :
                               _moveLeftMcts.MovePlayerLeft(player2pos);
                               precedenteAction = 1;
                               break;
                           case 2 : 
                               _moveRightMcts.MovePlayerRight(player2pos);
                               precedenteAction = 2;
                               break;
                           case 3 : 
                               _kickMcts.Kick(player2pos);
                               precedenteAction = 3;
                               break;
                           case 4 :
                               _punchMcts.Punch(player2pos);
                               precedenteAction = 4;
                               break;
                           case 5 :
                               _jumpMcts.Jump(player2pos);
                               precedenteAction = 5;
                               break;
                       }
                   }
                   else
                   {
                       int rand = Random.Range(1, 5);
           
                       switch (rand)
                       {
                           case 1:
                               _moveLeftMcts.MovePlayerLeft(player2pos);
                               precedenteAction = 1;
                               break;
                           case 2:
                               _moveRightMcts.MovePlayerRight(player2pos);
                               precedenteAction = 2;
                               break;
                           case 3:
                               _kickMcts.Kick(player2pos);
                               precedenteAction = 3;
                               break;
                           case 4:
                               _punchMcts.Punch(player2pos);
                               precedenteAction = 4;
                               break;
                       }
                   } 
        }
        else
        {
            ResultSimu(totalResult);
            compteur++;
        }
    }
    
    
    public void TotalResultat(int resultat)
    {
        totalResult += resultat;
        nbTotal++;
    }

    public void ResultSimu(float totalResult)
    {
        totalpourcentage = (totalResult / nbTotal)* 100;
    }
   public int EndGame(int code)
    {
        return code;
    }
}
