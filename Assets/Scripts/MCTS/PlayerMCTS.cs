using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMCTS : MonoBehaviour
{
    ActionMCTS Action;
    int x, y, health;
    public PlayerMCTS(int posx, int posy)
    {
        this.x = posx;
        this.x = posy;
        Action = new ActionMCTS();
    }
}
