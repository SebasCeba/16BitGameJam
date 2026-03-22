using TMPro;
using UnityEngine;

public class DistanceToCollectible : MonoBehaviour
{
    [SerializeField] private Player playa;
    [SerializeField] private TextMeshProUGUI distanceText;
    [SerializeField] private string collectibleTag = "Collectible";

    [SerializeField] private float closeThreshold; // Distance at which the text changes to "Close!"
    [SerializeField] private float nearThreshold; // Distance at which the text changes to "Near!"
    // Update is called once per frame
    void Update()
    {
        // Find all active collectibles in the scnee 
        GameObject[] collectibles = GameObject.FindGameObjectsWithTag(collectibleTag);

        if(collectibles.Length == 0)
        {
            distanceText.text = "All collected!";
            return;
        }

        // Find the closest collectible
        float minDistance = float.MaxValue;
        foreach (GameObject collectible in collectibles)
        {
            float distance = Vector2.Distance(playa.transform.position, collectible.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
            }
        }

        // Set the text based on distance 
        if(minDistance <= closeThreshold)
            distanceText.text = "Close!";
        else if (minDistance <= nearThreshold)
            distanceText.text = "Near!";
        else
            distanceText.text = "Far!";
    }
}
