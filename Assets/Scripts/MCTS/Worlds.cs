using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worlds : MonoBehaviour
{
   private int taillemap = 300;
   private Vector2 posJ1 = new Vector2(0, 0);
   private Vector2 posJ2 = new Vector2(300, 0);
   private int ValMove = 5;
   private bool moveRight = false;
   private bool moveLeft = false;


   void MovePlayerRight()
   {
       if (moveRight ==true && posJ2.x < 300)
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
