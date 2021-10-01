using UnityEngine;

public class JumpMCTS : MonoBehaviour
{
    public bool jumpActif = true;

    public void Jump(Vector2 player)
    {
        // Si on est au sol on peut jump 
        if (player.y == 1.64684) player += new Vector2(0, 3);
    }

    public void JumpActive()
    {
        jumpActif = !jumpActif;
    }

    // Renvoie si l'action est disponible ou pas 
    public bool JumpIsActive()
    {
        return jumpActif;
    }
}
