using UnityEngine;
using UnityEngine.SceneManagement;

public class killBox : MonoBehaviour
{
    Player player;
    private void Awake()
    {
        player = FindAnyObjectByType<Player>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.playerDeathAnim();
            Invoke(nameof(ReloadSceneFromManager), player.deathDelay);
        }
    }
    private void ReloadSceneFromManager()
    {
        GameManager.Instance.ReloadScene();
    }
}
