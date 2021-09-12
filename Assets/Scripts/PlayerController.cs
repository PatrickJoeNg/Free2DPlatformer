using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //parameters
    [Header("General Parameters")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] int jumpCounter = 0;
    [SerializeField] float jumpForce = 20f;
    [SerializeField] float climbSpeed = 5f;

    [Space]
    [Header("Character Attributes")]
    public int health;
    public int maxHealth;

    [Space]
    [Header("Death Attributes")]
    [SerializeField] Vector2 deathPushback = new Vector2();

    //State
    float gravityScale;
    bool isAlive;

    // cached
    Animator characterAnimator;
    Rigidbody2D rb;
    BoxCollider2D feetCollider;
    CapsuleCollider2D bodyCollider;

    Damage dmg;
    public HealthBar healthBar;
    private float xMove, yMove;

    private void Start()
    {
        //player state
        isAlive = true;

        // caching components
        rb = GetComponent<Rigidbody2D>();
        characterAnimator = GetComponent<Animator>();
        feetCollider = GetComponent<BoxCollider2D>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        dmg = GetComponent<Damage>();

        // set gravity to normal
        gravityScale = rb.gravityScale;

        //init health
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        if (!isAlive) return;

        if (PauseMenu.GameIsPaused)
        {
            return;
        }
        else
        {
            ProcessJump();
            ProcessMovement();
        }
        TurnAround();
        ClimbLadder();
        CheckIfGrounded();
        DebuggingInputs();
    }

    private void DebuggingInputs()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        int damageAmt = dmg.GetDamage();

        health -= damageAmt;
        healthBar.SetHealth(health);

        if (health <= 0)
        {
            Death();
        }
    }

    void CheckIfGrounded()
    {
        if (feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            jumpCounter = 0;
            characterAnimator.SetBool(TagManager.ISJUMPING_ANIMATION_NAME, false);
            characterAnimator.SetBool(TagManager.ISFALLING_ANIMATION_NAME, false);
            characterAnimator.SetBool(TagManager.CANDOUBLE_ANIMATION_NAME, false);
        }
    }

    void ProcessMovement()
    {
        xMove = Input.GetAxis(TagManager.HORIZONTAL_TAG) * moveSpeed;

        Vector2 playerVelocity = new Vector2(xMove, rb.velocity.y);
        rb.velocity = playerVelocity;

        bool playerIsMoving = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        characterAnimator.SetBool(TagManager.ISRUNNING_ANIMATION_NAME, playerIsMoving);
    }

    void ProcessJump()
    {
        bool jumping = rb.velocity.y > 0;
        bool falling = rb.velocity.y < 0;
        bool canDoubleJump = jumpCounter == 1;

        characterAnimator.SetBool(TagManager.ISJUMPING_ANIMATION_NAME, jumping);
        characterAnimator.SetBool(TagManager.ISFALLING_ANIMATION_NAME, falling);
        characterAnimator.SetBool(TagManager.CANDOUBLE_ANIMATION_NAME, canDoubleJump);

        // Prevent infinite jumps
        if (jumpCounter >= 1 && !feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpCounter++;

            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void TurnAround()
    {
        bool playerIsMoving = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;

        if (playerIsMoving)
        {
            transform.localScale = new Vector3(
                Mathf.Abs(transform.localScale.x) * Mathf.Sign(rb.velocity.x),
                transform.localScale.y);
        }
    }
    void ClimbLadder()
    {
        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            rb.gravityScale = gravityScale;
            return;
        }

        characterAnimator.SetBool(TagManager.ISJUMPING_ANIMATION_NAME, false);
        characterAnimator.SetBool(TagManager.ISFALLING_ANIMATION_NAME, false);
        characterAnimator.SetBool(TagManager.CANDOUBLE_ANIMATION_NAME, false);

        float yMove = Input.GetAxis(TagManager.HORIZONTAL_TAG);
        Vector2 climbVelocity = new Vector2(rb.velocity.x, yMove * climbSpeed);
        rb.velocity = climbVelocity;

        rb.gravityScale = 0f;

        bool playerIsClimbing = Mathf.Abs(rb.velocity.y) > Mathf.Epsilon;
        characterAnimator.SetBool(TagManager.ISRUNNING_ANIMATION_NAME, playerIsClimbing);
    }

    private void Death()
    {
        health = 0;
        isAlive = false;

        characterAnimator.SetTrigger(TagManager.DYING_ANIMATION_NAME);
        rb.velocity = deathPushback;

        GameManager.instance.GameOver();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            transform.parent = other.gameObject.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        transform.parent = null;
    }

}
