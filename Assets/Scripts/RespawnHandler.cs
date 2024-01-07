using UnityEngine;

public class RespawnHandler : MonoBehaviour
{
    public void RespawnPlayer()
    {
        // Find the player and call the Respawn method from the Health script
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Health playerHealth = player.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.Respawn();
            }
        }
    }
}
