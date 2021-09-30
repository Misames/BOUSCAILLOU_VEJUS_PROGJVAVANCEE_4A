using System;
using UnityEngine;

public class Worlds : MonoBehaviour
{
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
}
