using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] public Rigidbody2D rb2D;   
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpingPower = 16f;
    private float horizontalInput;

    private bool isFacingRight = true;

    [Header("Ground Check Components")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Vector2 groundCheckBoxSize = new Vector2(5.5f, 0.1f); // Adjust the size of the box as needed

    [Header("Animation")]
    [SerializeField] public Animator anim;

    [Header("Death Settings")]
    public float deathDelay = 2.5f; // Time to wait before reloading the scene after death
    public bool hasDied = false;

    [Header("Celebration Settings")]
    public bool isCelebrating = false;

    [Header("Managers")]
    public AudioManager audioManager;
    public CollectibleManager cm;

    [Header("Respawn Settings")]
    Vector2 checkpointPos; // Player's original position when they spawn in the game 
    public float deathDuration; // This should affect the duration in the coroutine and animation.
    public int checkpointLimit = 3;
    public bool canRespawn = true;
    public GameObject checkpointObject; // Using the prefab of the checkpoint object. 
    public GameObject checkpointTransform; // We need to make sure that the checkpoint will spawn on or above us/ the ground. 

    private void Awake()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
        anim.SetBool("Death", false);
        isCelebrating = false;
        rb2D = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        checkpointPos = transform.position; // Set the start position to the player's current position at the start of the game
        checkpointLimit = 3;
        canRespawn = true; // Allow the player to respawn at the start of the game
    }
    private void Update()
    {
        if(hasDied || isCelebrating || !canRespawn) return; // Skip the rest of the update if the player has died, is celebrating, or cannot respawn

        horizontalInput = Input.GetAxisRaw("Horizontal");

        bool isMoving = Mathf.Abs(horizontalInput) > 0.1f && IsGrounded() && !hasDied && !isCelebrating;

        anim.SetFloat("Speed", Mathf.Abs(horizontalInput));

        // This checks if the player is touching the ground and checks if it could.
        if(Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpingPower);
            anim.SetBool("Jumping", true);

            audioManager.PlayRandomJumpSfx();
        }

        // Depending on how long or short the player holds the button, they longer/high they jump. 
        if (Input.GetButtonUp("Jump") && rb2D.velocity.y > 0f)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, rb2D.velocity.y * 0.5f);
            anim.SetBool("Jumping", true);
        }

        Flip();

        // Check for checpoint respawn input
        respawnLimit();

        anim.SetBool("Jumping", !IsGrounded());
    }
    private void FixedUpdate()
    {
        rb2D.velocity = new Vector2(horizontalInput * speed, rb2D.velocity.y);
    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapBox(groundCheck.position, groundCheckBoxSize, 0f, groundLayer);
    }
    private void OnDrawGizmosSelected()
    {
        if(groundCheck == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheck.position, new Vector3(groundCheckBoxSize.x, groundCheckBoxSize.y, 1f)); // Adjust the size of the box as needed 
    }
    private void Flip()
    {
        if(isFacingRight && horizontalInput < 0f || !isFacingRight && horizontalInput > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale; 
        }
    }
    public void playerDeathAnim()
    {
        anim.SetBool("Death", true);
        hasDied = true;
        audioManager.PlaySfx(audioManager.deathSfx);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Collectible"))
        {
            audioManager.PlaySfx(audioManager.collectedSfx);
            Destroy(other.gameObject);
            cm.collectCount++; 
        }
        if (other.gameObject.CompareTag("Lever"))
        {
            other.gameObject.GetComponent<Animator>().SetBool("Enter", true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Lever"))
        {
            other.gameObject.GetComponent<Animator>().SetBool("Exit", true);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            Die();
        }
    }
    public void UpdateCheckpoint(Vector2 pos)
    {
        checkpointPos = pos; // Update the checkpoint position to the new position passed in
    }
    public void respawnLimit()
    {
        if(checkpointLimit <= 0) return; // If the checkpoint limit has been reached, do not allow the player to respawn

        if (Input.GetKeyUp(KeyCode.I))
        {
            checkpointLimit--;
            GameObject checkpoint = Instantiate(checkpointObject, 
                checkpointTransform.transform.position, Quaternion.identity); // Spawn the checkpoint object at the specified position

            checkpointPos = checkpoint.transform.position; // Update the checkpoint position to the new checkpoint's position
        }
    }
    void Die()
    {
        StartCoroutine(Respawn(deathDuration)); // Call the Respawn method to reset the player's position
    }
    IEnumerator Respawn(float duration)
    {
        playerDeathAnim(); // Play the player's death animation
        rb2D.simulated = false; // Disable the player's Rigidbody2D simulation to prevent any physics interactions while the player is "dead"
        rb2D.velocity = new Vector2(0, 0); // Reset the player's velocity to zero to stop any movement while the player is "dead"
        yield return new WaitForSeconds(duration); // Wait for the specified duration before respawning the player
        transform.position = checkpointPos; // Reset the player's position to the original start position
        rb2D.simulated = true; // Re-enable the player's Rigidbody2D simulation to allow the player to interact with the game world again
        
        anim.SetBool("Death", false); // Reset the death animation state
        hasDied = false; // Reset the player's death status to allow them to play again
    }
    public void TeleportToCheckpoint()
    {
        transform.position = checkpointPos; // Teleport the player to the current checkpoint position
    }
}
