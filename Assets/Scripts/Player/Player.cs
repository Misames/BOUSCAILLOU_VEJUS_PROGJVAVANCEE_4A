using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    [SerializeField] GameObject healthBar;
    [SerializeField] float coefBlock = 1f;
    public int health = 100;
    bool block;

    void Start()
    {
        healthBar.GetComponent<Slider>().value = health;
    }

    public void Punch(Player enemie)
    {
        Debug.Log("Coup de poing !");
        enemie.health -= 10;
        enemie.healthBar.GetComponent<Slider>().value = enemie.health;
    }

    public void Kick(Player enemie)
    {
        Debug.Log("Coup de pied !");
        enemie.health -= 20;
        enemie.healthBar.GetComponent<Slider>().value = enemie.health;
    }

    public void Block()
    {
        Debug.Log("Blocage !");
        block = true;
    }
}
