using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchMCTS : MonoBehaviour
{
    public float punchCd = 0.5f;
    public Vector2 hitCol;
    public int j2Life = 100;
    public Vector2 posJ2;
    public bool isFinish = false;
    public bool punchActif = true;
    
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

    public void Punch()
    {
        // Si on peut utiliser le punch
        if (punchActif == true)
        {
            if (posJ2.x == hitCol.x && posJ2.y == hitCol.y)
            {
                j2Life -= 20;
                if (j2Life <= 0)
                {
                    isFinish = true;
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
