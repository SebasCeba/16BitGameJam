using TMPro;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    public Player player;

    [Header("Collectible Settings")]
    public int collectCount;
    public int collectCountTotal;
    public TextMeshProUGUI collectText;

    [Header("Block Placement Settings")]
    public int blockPlaceCount;
    public TextMeshProUGUI blockPlaceText;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        collectText.text = "Total: " + collectCountTotal.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        int remaining = collectCountTotal - collectCount;
        if(collectCount == 0)
        {
            // Show the total count at the start 
            collectText.text = "Total: " + collectCountTotal.ToString();
        }
        else if (remaining > 0)
        {
            // Show how many are left to collect 
            collectText.text = "Left: " + remaining.ToString();
        }
        else
        {
            // All collected 
            collectText.text = "All Collected!";
        }
        
        //collectText.text = ": " + collectCount.ToString();

        if(collectCount >= collectCountTotal)
        {
            player.isCelebrating = true;
            player.anim.SetBool("Celebrate", true);
        }

        blockPlaceText.text = "Placed: " + blockPlaceCount.ToString();
    }
    public void blockUpdate()
    {
        blockPlaceCount++;
    }
}
