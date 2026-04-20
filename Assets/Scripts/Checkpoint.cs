using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    Player playerCont;
    public Transform respawnPoint; // The position where the player will respawn after reaching this checkpoint

    private void Awake()
    {
        playerCont = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerCont.UpdateCheckpoint(respawnPoint.position);
        }
    }
}
