using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchMCTS : MonoBehaviour
{
    public float punchCd = 0.5f;
    public Vector2 hitCol;
    public Vector2 posJ2;
    public bool isFinish = false;
    public bool punchActif = true;
    public GameManagerMCTS _gameManagerMcts;
    
    private void Start()
    {
        // Initialisation du Collider Virtuel pour le hit damage
        hitCol = new Vector2(posJ2.x +1 ,posJ2.y +1);
    }
    
    private void Update()
    {
        // Lancement Cooldown punch
        if (punchActif == true)
        {
            punchCd -= Time.deltaTime;
        }
        // reset Cooldown Punch
        if (punchCd <= 0)
        {
            punchCd = 0.5f;
        }
    }

    public void Punch(Vector2 player)
    {
        
            // Initialisation du Collider Virtuel pour le hit damage
            hitCol = new Vector2(player.x +1 ,player.y +1);
            // Si on peut utiliser le Kick
            if (punchActif == true)
            {
                if (player.x == hitCol.x && player.y == hitCol.y)
                {
                    _gameManagerMcts.lifeJ1 -= 20;
                    if (_gameManagerMcts.lifeJ1 <= 0)
                    {
                        isFinish = true;
                        _gameManagerMcts.TotalResultat(1); 
                    }
                    
                }
        }
    }
    
    // Active ou desactive la possibilité de punch
    public void PunchActive()
    {
        punchActif = !punchActif;
    }
   
    // Renvoie si l'action est disponible ou pas 
    
    public bool PunchIsActive()
    {
        return punchActif;
    }
}
