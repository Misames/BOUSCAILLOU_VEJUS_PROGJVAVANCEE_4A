using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManagerMCTS : MonoBehaviour
{
    public PlayerVSMCTS player1;
    [HideInInspector]
    public Vector2 player1pos;
    [HideInInspector]
    public Vector2 player2pos;
    [HideInInspector]
    public int lifeJ1;
    [HideInInspector]
    public int lifeJ2;
    float time, timeDuration;
    private int bestActions = -1;
    private int totalResult;
    private float totalpourcentage;
    private int nbTotal;
    private int precedenteAction;
    public MoveLeftMCTS _moveLeftMcts;
    public MoveRightMCTS _moveRightMcts;
    public JumpMCTS _jumpMcts;
    public KickMCTS _kickMcts;
    public PunchMCTS _punchMcts;
    private bool endSimu = false;
    private int compteur = 0;
    public PlayerMCTS _playerMCTS;
    
    
    
    
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject endScreen;
    [SerializeField] GameObject txtEnd;
    [SerializeField] Text textTime;
    [SerializeField] float gameDuration = 60;
    bool isPause;
    
   

    private void Start()
    {
        //init game
        Time.timeScale = 1f;
        isPause = false;
        
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
        
        gameDuration -= Time.deltaTime;
        textTime.text = Mathf.RoundToInt(gameDuration).ToString();
        if (gameDuration <= 0)
        {
            if (player1.myHealth > _playerMCTS.myHealth) endGame("Joueur 1");
            else if (player1.myHealth < _playerMCTS.myHealth) endGame("Joueur 2");
            else endGame("");
        }
        if (player1.myHealth <= 0) endGame("Joueur 2");
        if (_playerMCTS.myHealth <= 0) endGame("Joueur 1");
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause) ResumeGame();
            else PauseGame();
        }
        
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
               
                _playerMCTS.isAttacking = true;
                _playerMCTS.animator.Play("PlayerPunch");
                StartCoroutine(_playerMCTS.DoAttack());
                if (_playerMCTS.inRange) _playerMCTS.Punch(player1);
                break;
            case 2:
               
                _playerMCTS.isAttacking = true;
                _playerMCTS.animator.Play("PlayerKick");
                StartCoroutine(_playerMCTS.DoAttack());
                if (_playerMCTS.inRange) _playerMCTS.Kick(player1);
                break;
            case 3:
                _playerMCTS.Move(5);
                _playerMCTS.ChangeDirection();
                break;
            case 4:
                _playerMCTS.Move(-5);
                _playerMCTS.ChangeDirection();
                break;
            case 5:
                _playerMCTS.isGrounding = Physics2D.OverlapArea(_playerMCTS.GroundCheckLeft.position, _playerMCTS.GroundCheckRight.position);
                if (_playerMCTS.isGrounding)
                {
                    _playerMCTS.animator.Play("PlayerJump");
                    _playerMCTS.RigidbodyPlayer.AddForce(new Vector2(0.0f, _playerMCTS.jumpforce));
                    _playerMCTS.isjumping = false;
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
   
   // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // //
   // // // // // //                                FONCTION MENU             // // // // // // // // // // // //
   // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // //
   public void endGame(string namePlayer)
   {
       Time.timeScale = 0f;
       endScreen.SetActive(true);
       Text monText = txtEnd.GetComponent<Text>();
       if (namePlayer != "") monText.text = $"Le gagant est {namePlayer}";
       else monText.text = "Match nul !";
   }

   public void ResumeGame()
   {
       pauseMenu.SetActive(false);
       Cursor.lockState = CursorLockMode.Locked;
       Cursor.visible = false;
       Time.timeScale = 1f;
       isPause = false;
   }

   public void PauseGame()
   {
       Time.timeScale = 0f;
       isPause = true;
       pauseMenu.SetActive(true);
       Cursor.lockState = CursorLockMode.None;
       Cursor.visible = true;
   }

   public void Replay()
   {
       SceneManager.LoadScene(SceneManager.GetActiveScene().name);
   }

   public void Quit()
   {
       SceneManager.LoadScene(0);
   }
   
   
   
   
}
