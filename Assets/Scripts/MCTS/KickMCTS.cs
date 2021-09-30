using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickMCTS : MonoBehaviour
{
    public float kickCd = 0.5f;
    public Vector2 hitCol;
    public bool isFinish = false;
    public bool kickActif = true;
    private GameManagerMCTS _gameManagerMcts;
    
    private void Start()
    {
        
    }
    
    private void Update()
    {
        // Lancement Cooldown kick
        if (kickActif == true)
        {
            kickCd -= Time.deltaTime;
        }
        // reset Cooldown kick
        if (kickCd <= 0)
        {
            kickCd = 0.5f;
        }
    }

   public void Kick(Vector2 player)
    {
        // Initialisation du Collider Virtuel pour le hit damage
        hitCol = new Vector2(player.x +1 ,player.y +1);
        // Si on peut utiliser le Kick
        if (kickActif == true)
        {
            if (player.x == hitCol.x && player.y == hitCol.y)
            {
                _gameManagerMcts.lifeJ1 -= 20;
                if (_gameManagerMcts.lifeJ1 <= 0)
                {
                    isFinish = true;
                    _gameManagerMcts.TotalResultat(1);
                }
            }
        }
    }
    
   // Active ou desactive la possibilité de Kick
   public void KickActive()
    {
        kickActif = !kickActif;
    }
   
    // Renvoie si l'action est disponible ou pas 
    
   public bool KickIsActive()
   {
       return kickActif;
   }
}
