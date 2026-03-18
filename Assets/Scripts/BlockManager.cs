using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class BlockManager : MonoBehaviour
{
    public Tilemap tileMap;
    public TileBase placeTile;

    public GameObject blockPrefab;
    public float gridSize = 1f;
    public LayerMask blockLayer;

    public CinemachineVirtualCamera vcam;

    private bool isPlacing = false;
    private bool isDeleting = false;
    bool actionUsed = false; 


    private void Update()
    {
        HandleModes();
        HandleBlockPlacement();
        HandleUtility();
    }

    void HandleModes()
    {
        // Enter placement mode 
        if (Input.GetKeyDown(KeyCode.U))
        {
            isPlacing = true;
            isDeleting = false;
            actionUsed = false;
        }

        // Enter delete mode 
        if(Input.GetKeyDown(KeyCode.I))
        {
            isDeleting = true;
            isPlacing = false;
            actionUsed = false;
        }

        // Exit modes (release keys)
        if (Input.GetKeyUp(KeyCode.U)) isPlacing = false;
        if (Input.GetKeyUp(KeyCode.I)) isDeleting = false;
    }

    void HandleBlockPlacement()
    {
        if((!isPlacing && !isDeleting) || actionUsed) return;

        Vector2 direction = GetDirectionInput();
        if(direction == Vector2.zero) return;

        if (isPlacing)
            PlaceBlock(direction); 

        if (isDeleting)
            DeleteBlock(direction);

        actionUsed = true; // Prevents spam 
    }

    Vector2 GetDirectionInput()
    {
        if(Input.GetKeyDown(KeyCode.W)) return Vector2.up;
        if (Input.GetKeyDown(KeyCode.A)) return Vector2.left;
        if (Input.GetKeyDown(KeyCode.D)) return Vector2.right;

        return Vector2.zero;
    }


    void PlaceBlock(Vector2 direction)
    {
        Vector3 worldPos = transform.position + (Vector3)direction;

        Vector3Int cellPos = tileMap.WorldToCell(worldPos);

        //Collider2D hit = Physics2D.OverlapBox(pos, Vector2.one * gridSize * 0.9f, 0f, blockLayer);

        //if(hit == null)
        //{
        //    Instantiate(blockPrefab, pos, Quaternion.identity);
        //}

        // Prevent placing blocks inside the player
        if (!tileMap.HasTile(cellPos))
        {
            tileMap.SetTile(cellPos, placeTile);
            tileMap.RefreshTile(cellPos);
        }
    }
    void DeleteBlock(Vector2 direction)
    {
        Vector3 worldPos = transform.position + (Vector3)direction;

        Vector3Int cellPos = tileMap.WorldToCell(worldPos);

        //Collider2D hit = Physics2D.OverlapBox(pos, Vector2.one * gridSize * 0.9f, 0f, blockLayer);

        //if (hit != null)
        //{
        //    Destroy(hit.gameObject);
        //}

        if (tileMap.HasTile(cellPos))
        {
            tileMap.SetTile(cellPos, null);
            tileMap.RefreshTile(cellPos);
        }
    }

    //Vector2 GetSnappedPosition(Vector2 direction)
    //{
    //    Vector2 rawPos = (Vector2)transform.position + direction;

    //    return new Vector2(
    //        Mathf.Round(rawPos.x / gridSize) * gridSize,
    //        Mathf.Round(rawPos.y / gridSize) * gridSize
    //    );
    //}

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
        return isPlacing || isDeleting;
    }
}
