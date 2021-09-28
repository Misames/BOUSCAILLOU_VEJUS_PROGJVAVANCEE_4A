using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    int health = 100;
    [SerializeField]
    float coefBlock = 1f;
    bool block;

    void Punch(Player enemie)
    {
        enemie.health -= 10;
        Debug.Log("Coup de poing !");
    }

    void Kick(Player enemie)
    {
        enemie.health -= 20;
        Debug.Log("Coup de pied !");
    }

    void Block()
    {
        Debug.Log("Blocage !");
        block = true;
    }
}
