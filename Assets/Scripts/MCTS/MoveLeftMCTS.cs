using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftMCTS : MonoBehaviour
{
    private Vector2 posJ2;
    public float ValMove;
    void MovePlayerLeft()
    {
        if (posJ2.x <= 0)
        {
            posJ2.x += 0;
        }
        else
        {
            posJ2.x -= ValMove;
        }
    }
}
