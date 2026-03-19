using UnityEngine;
using UnityEngine.SceneManagement;

public class killBox : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadSceneAsync(0);
        }
    }
}
