using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickMCTS : MonoBehaviour
{
    public float kickCd = 0.5f;
    public Vector2 hitCol;
    public int j2life = 100;
    public Vector2 posJ2;
    public bool isFinish = false;
    public bool kickActif = true;
    
    private void Start()
    {
        // Initialisation du Collider Virtuel pour le hit damage
        hitCol = new Vector2(posJ2.x +1 ,posJ2.y +1);
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

   public void Kick()
    {
        // Si on peut utiliser le Kick
        if (kickActif == true)
        {
            if (posJ2.x == hitCol.x && posJ2.y == hitCol.y)
            {
                j2life -= 20;
                if (j2life <= 0)
                {
                    isFinish = true;
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
