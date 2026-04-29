using UnityEngine;
using UnityEngine.Tilemaps;
using Cinemachine;

public class BlockManager : MonoBehaviour
{
    public Tilemap playerBlockTile;
    public TileBase placeTile;
    private Vector2 lastDirection = Vector2.zero;

    public CinemachineVirtualCamera vcam;

    public float placeRange = 10f;

    public float FOVRegular;
    public float FOVZoom;

    public CollectibleManager cm;
    public AudioManager audioManager;
    public Player player; 
    private void Update()
    {
        //HandleDelete();
        HandlePlacement();
        HandleUtility();
    }
    //void HandlePreview()
    //{
    //    Vector2 direction = GetDirectionInput();
    //    if (direction != Vector2.zero)
    //        lastDirection = direction;

    //    if (lastDirection != Vector2.zero)
    //    {
    //        Vector3 worldPos = transform.position + (Vector3)lastDirection * placeRange;
    //        Vector3Int cellPos = playerBlockTile.WorldToCell(worldPos);

    //        // Only update if changed 
    //        if (!previewCell.HasValue || previewCell.Value != cellPos)
    //        {
    //            // Clear previous preview 
    //            if (previewCell.HasValue && playerBlockTile.GetTile(previewCell.Value) == previewTile)
    //            {
    //                playerBlockTile.SetTile(previewCell.Value, null);
    //            }
    //            // Set new preview if valid
    //            if (!playerBlockTile.HasTile(cellPos))
    //            {
    //                playerBlockTile.SetTile(cellPos, previewTile);
    //                previewCell = cellPos;
    //            }
    //            else
    //            {
    //                previewCell = null; // Invalid position, clear preview
    //            }
    //        }
    //    }
    //}

    private void HandlePlacement()
    {
        Vector2 direction = GetDirectionInput();
        if (direction != Vector2.zero)
            lastDirection = direction;

        if(Input.GetKeyDown(KeyCode.U) && lastDirection != Vector2.zero)
        {
            Vector3 worldPos = transform.position + (Vector3)lastDirection * placeRange;
            Vector3Int cellPos = playerBlockTile.WorldToCell(worldPos);

            audioManager.PlayRandomPlacementSfx();

            if (!playerBlockTile.HasTile(cellPos))
            {
                playerBlockTile.SetTile(cellPos, placeTile);
                playerBlockTile.RefreshTile(cellPos);

                cm.blockUpdate();
            }
        }
    }
    //private void HandleDelete()
    //{
    //    // Delete a block at the player's feet when I is pressed.
    //    if (Input.GetKeyDown(KeyCode.I))
    //    {
    //        // Find the player in the scene
    //        GameObject player = GameObject.FindGameObjectWithTag("Player");
    //        if (player != null)
    //        {
    //            // Use the bottom of the player's collider for accuracy
    //            Vector3 feetPos = player.transform.position + Vector3.down * 0.5f;
    //            Vector3Int cellPos = playerBlockTile.WorldToCell(feetPos);
    //            if (playerBlockTile.HasTile(cellPos))
    //            {
    //                playerBlockTile.SetTile(cellPos, null);
    //                playerBlockTile.RefreshTile(cellPos);
    //            }
    //        }
    //    }
    //}

    private Vector2 GetDirectionInput()
    {
        if(Input.GetKey(KeyCode.W)) return Vector2.up;
        if (Input.GetKey(KeyCode.A)) return Vector2.left;
        if (Input.GetKey(KeyCode.D)) return Vector2.right;

        return Vector2.zero;
    }
    private void HandleUtility()
    {
        // Teleport to the checkpoint 
        if(Input.GetKeyDown(KeyCode.J))
        {
            player.TeleportToCheckpoint();
        }
        // Restart scene
        if (Input.GetKeyDown(KeyCode.K))
        {
            GameManager.Instance.ReloadScene();
        }

        // Zoom toggle
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (vcam.m_Lens.OrthographicSize == FOVRegular)
                vcam.m_Lens.OrthographicSize = FOVZoom;
            else
                vcam.m_Lens.OrthographicSize = FOVRegular;
        }
    }
}
