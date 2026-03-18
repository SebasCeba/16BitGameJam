using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class BlockManager : MonoBehaviour
{
    public Tilemap playerBlockTile;
    public TileBase placeTile;

    //public TileBase previewTile;
    //private Vector3Int? previewCell = null;

    private Vector2 lastDirection = Vector2.zero;

    public CinemachineVirtualCamera vcam;

    public float placeRange = 10f;
    //public float placeCooldown = 0f;
    //private float lastPlaceTime = 0f; 


    private void Update()
    {
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

    void HandlePlacement()
    {
        Vector2 direction = GetDirectionInput();
        if (direction != Vector2.zero)
            lastDirection = direction;

        if(Input.GetKeyDown(KeyCode.U) && lastDirection != Vector2.zero)
        {
            Vector3 worldPos = transform.position + (Vector3)lastDirection * placeRange;
            Vector3Int cellPos = playerBlockTile.WorldToCell(worldPos);

            if (!playerBlockTile.HasTile(cellPos))
            {
                playerBlockTile.SetTile(cellPos, placeTile);
                playerBlockTile.RefreshTile(cellPos);
            }
        }
    }

    Vector2 GetDirectionInput()
    {
        if(Input.GetKey(KeyCode.W)) return Vector2.up;
        if (Input.GetKey(KeyCode.A)) return Vector2.left;
        if (Input.GetKey(KeyCode.D)) return Vector2.right;

        return Vector2.zero;
    }
    void HandleUtility()
    {
        // Restart scene
        if (Input.GetKeyDown(KeyCode.J))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        // Zoom toggle
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (vcam.m_Lens.OrthographicSize == 50f)
                vcam.m_Lens.OrthographicSize = 75f;
            else
                vcam.m_Lens.OrthographicSize = 50f;
        }
    }

    public bool IsBusy()
    {
        return false;
    }
}
