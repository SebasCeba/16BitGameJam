using TMPro;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    public int collectCount;
    public TextMeshProUGUI collectText;
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        collectText.text = ": " + collectCount.ToString();

        if(collectCount >= 1)
        {
            player.isCelebrating = true;
            player.anim.SetBool("Celebrate", true);
        }
    }
}
