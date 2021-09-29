using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Random = UnityEngine.Random;


public class MCTS : MonoBehaviour
{
    private actions[] listActions;
    public int nbCoup ;
    public int result;
    private bool isGameFinish;

    private void Start()
    {
        listActions = new actions[4];
        listActions[0] = moveRight;
        listActions[1] = moveLeft;
        listActions[2] = Jump;
        listActions[3] = Punch;
        listActions[4] = kick;
    }

    void returnresult()
    {
        if (result == 1)
        {
            nbactions(1);
        } else if (result == -1)
        {
            nbactions(-1);
        } else if (result == 0)
        {
            nbactions(0);
        }
    }
    
    void nbactions(int resultat)
    {
        nbCoup++;
        result += resultat;
    }

    float tauxVict()
    {
        if (result != 0)
        {
            return result / nbCoup;
        }
        else
        {
            return 0;
        }
        
    }

    void ComputeMCTS()
    {
        int resultat = 0;
        Action bestActions = /* Jump , moveRight , moveLeft , punch , kick */ ;
        foreach (listAction in )
        {
            
        }

    }
}
