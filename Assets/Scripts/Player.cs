using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public Rigidbody2D rb2D;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] public Animator anim;

    private float horizontalInput;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpingPower = 16f; 
    private bool isFacingRight = true;

    public bool hasDied = false; 
    public bool isCelebrating = false;
    public float deathDelay = 2.5f; // Time to wait before reloading the scene after death

    //AudioManager audioManager;
    public CollectibleManager cm;

    private void Awake()
    {
        //audioManager = FindAnyObjectByType<AudioManager>();
        anim.SetBool("Death", false);
        isCelebrating = false;
    }
    private void Update()
    { 
        if (hasDied) return; // Skip the rest of the update if the player has died
        if(isCelebrating) return; // Skip the rest of the update if the player is celebrating

        horizontalInput = Input.GetAxisRaw("Horizontal");

        anim.SetFloat("Speed", Mathf.Abs(horizontalInput));

        // This checks if the player is touching the ground and checks if it could.
        if(Input.GetKeyDown(KeyCode.J) && IsGrounded())
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpingPower);
            anim.SetBool("Jumping", true);

            //audioManager.PlaySfx(audioManager.jumpSfx);
        }

        // Depending on how long or short the player holds the button, they longer/high they jump. 
        if (Input.GetKeyUp(KeyCode.J) && rb2D.velocity.y > 0f)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, rb2D.velocity.y * 0.5f);
            anim.SetBool("Jumping", true);
        }

            Flip();

        anim.SetBool("Jumping", !IsGrounded());
    }

    private void FixedUpdate()
    {
        rb2D.velocity = new Vector2(horizontalInput * speed, rb2D.velocity.y);
    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
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
        //audioManager.PlaySfx(audioManager.deathSfx);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Collectible"))
        {
            Destroy(other.gameObject);
            cm.collectCount++; 
        }
    }
}
