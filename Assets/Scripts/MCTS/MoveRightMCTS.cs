using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRightMCTS : MonoBehaviour
{
    private Vector2 posJ2;
    public float ValMove;
    void MovePlayerRight()
    {
        if (posJ2.x >= 20)
        {
            posJ2.x += 0;
        }
        else
        {
            posJ2.x += ValMove;
        }
    }

}
