using TMPro;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    public Player player;
    public BlockManager blockManager;

    [Header("Collectible Settings")]
    public int collectCount;
    public int collectCountTotal;
    public TextMeshProUGUI collectText;
    public TextMeshProUGUI blockScoreText; 

    [Header("Block Placement Settings")]
    public int blockPlaceCount;
    public TextMeshProUGUI blockPlaceText;
    public GameObject nextLevelUI;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        collectText.text = "Total: " + collectCountTotal.ToString();
        nextLevelUI.SetActive(false);
        blockManager.enabled = true; // Enable block placement at the start of the game
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
            player.anim.SetBool("Jumping", false); // Stop the jump animation if it's playing
            nextLevelUI.SetActive(true);
            player.anim.SetBool("Celebrate", true);
            player.rb2D.simulated = false; // Disable physics to prevent movement during celebration
            blockManager.enabled = false; // Disable block placement during celebration
            BlockScore();
        }

        blockPlaceText.text = "Placed: " + blockPlaceCount.ToString();
    }
    public void blockUpdate()
    {
        blockPlaceCount++;
    }

    public void BlockScore()
    {
        if(blockPlaceCount == 0)
        {
            blockScoreText.text = "How did you win without placing any blocks?";
        }
        else if(blockPlaceCount >= 50)
        {
            blockScoreText.text = "Woah, try again with fewer blocks next time.";
        }
        else if(blockPlaceCount >= 35)
        {
            blockScoreText.text = "You placed a lot of blocks! You must have had a hard time.";
        }
        else if(blockPlaceCount >= 20)
        {
            blockScoreText.text = "This seems reasonable amount of blocks placed.";
        }
        else
        {
            blockScoreText.text = "Nice job! You used very few blocks!";
        }
    }
}
