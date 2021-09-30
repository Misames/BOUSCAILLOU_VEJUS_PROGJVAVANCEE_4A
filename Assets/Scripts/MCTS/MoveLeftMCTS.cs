using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftMCTS : MonoBehaviour
{
    private Vector2 posJ2;
    public float ValMove;
    public void MovePlayerLeft(Vector2 player )
    {
        if (posJ2.x <= 0)
        {
            posJ2.x = player.x * 300 * Time.deltaTime;
        }
        else
        {
            posJ2.x -= ValMove;
        }
    }
}
