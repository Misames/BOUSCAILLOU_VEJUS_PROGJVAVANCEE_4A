using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRightMCTS : MonoBehaviour
{
    private Vector2 posJ2;
    public float ValMove;
    public void MovePlayerRight(Vector2 player)
    {
        if (posJ2.x >= 20)
        {
            posJ2.x = player.x * 300 * Time.deltaTime;
        }
        else
        {
            posJ2.x += ValMove;
        }
    }

}
