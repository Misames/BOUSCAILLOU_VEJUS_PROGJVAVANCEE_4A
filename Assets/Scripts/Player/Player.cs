using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    [SerializeField] float coefBlock = 1f;
    [SerializeField] GameObject healthBar;
    public int health = 100;
    bool block;

    void Start()
    {
        healthBar.GetComponent<Slider>().value = health;
    }

    void Punch(Player enemie)
    {
        Debug.Log("Coup de poing !");
        enemie.health -= 10;
        enemie.healthBar.GetComponent<Slider>().value = enemie.health;
    }

    void Kick(Player enemie)
    {
        Debug.Log("Coup de pied !");
        enemie.health -= 20;
        enemie.healthBar.GetComponent<Slider>().value = enemie.health;
    }

    void Block()
    {
        Debug.Log("Blocage !");
        block = true;
    }
}
