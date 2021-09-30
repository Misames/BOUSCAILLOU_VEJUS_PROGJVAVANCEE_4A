using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worlds : MonoBehaviour
{
   private int taillemap = 300;
   private Vector2 posJ1;
   private Vector2 posJ2;
   private int ValMove = 5;
   private bool moveRight = false;
   private bool moveLeft = false;
   private int J1life = 100;
   
   private float totalResult;
   private float totalpourcentage;
   private int nbTotal;
   void TotalResultat(int resultat)
   {
       totalResult += resultat;
       nbTotal++;
   }

   void ResultSimu(float totalResult)
   {
       totalpourcentage = totalResult / nbTotal;
   }

   private void Start()
   {
       posJ1 = new Vector2(0, 0);
       posJ2 = new Vector2(20, 0);
   }
   
   void MovePlayerRight()
   {
       if (moveRight ==true && posJ2.x < 20)
       {
           posJ2.x += ValMove;
       }
   }

   void MovePlayerLeft()
   {
        if (moveLeft == true && posJ2.x> 0)
        {
            posJ2.x -= ValMove;
        }
   }
}
