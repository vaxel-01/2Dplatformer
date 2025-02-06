using UnityEngine;

public class Player : MonoBehaviour
{
    
    //Player.Instance
    #region
    public static Player Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    #endregion

    [Header("Speed and power")]
    public float speed = 1.0f;
    public float jumpingPower = 16f;

    private float horizontal;
    private bool isFacingRight = true;

    [Header("Player Body")]

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private Vector2 startingPosition;

    [Header("YES ANIMATION LETS GO")]

    public Animator animator;
    public string currentAnimation;
    private string state;

    private bool isJumping = false;
    public bool isAttacking = false;
    private bool isHurt = false;


    private const string IDLE = "Player_Idle2";
    private const string RUN = "Player_Run2";
    private const string JUMP = "Player_Jump2";
    private const string ATTACK = "Player_Attack";
    private const string HURT = "Player_Hurt";
    //[SerializeField] private const string DIE = "";

    public int playerHealth; //total health
    public int currentHealth;

    public float timeStunned;
    private float stunnedTimer = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.instance.onGamePlay.AddListener(StartingGame);
        GameManager.instance.onGameEnd.AddListener(GameOver);
        startingPosition = transform.position;

        currentHealth = playerHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isPlaying)
        {
            if (!isHurt)
            {
                horizontal = Input.GetAxis("Horizontal");


                if (horizontal != 0 && !isJumping && !isAttacking)
                {
                    state = RUN;
                }

                if (Input.GetButtonDown("Jump") && IsGrounded() && !isAttacking)
                {
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);

                    isJumping = true;

                }
                if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0)
                {
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
                }
                if (IsGrounded() && isJumping)
                {
                    isJumping = false;
                }
                if (rb.linearVelocityY != 0)
                {
                    state = JUMP;
                }


                if (IsGrounded() && !isJumping && !isAttacking && horizontal == 0)
                {
                    state = IDLE;
                }

                if (Input.GetButtonDown("Fire1") && !isAttacking)
                {
                    isAttacking = true;
                    state = ATTACK;
                }
            }
            else
            {

                stunnedTimer += Time.deltaTime;
                if (stunnedTimer > timeStunned)
                {
                    isHurt = false;
                    rb.linearVelocity = Vector2.zero;
                    stunnedTimer = 0;
                }
            }

            Flip();
            PlayAnimationState();
        }

    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);

    }


    private void Flip()
    {
        if (isFacingRight && horizontal < 0 || !isFacingRight && horizontal > 0)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy") //Get hurt
        {
            Hurt();
        }
    }

    public void Hurt()
    {
        currentHealth--;
        UImanager.Instance.UpdateHealthbar(currentHealth.ToString());
        isHurt = true;
        state = HURT;
        rb.linearVelocity = new Vector2(-rb.linearVelocity.x, rb.linearVelocity.y);
        if (currentHealth == 0)
        {
            GameManager.instance.whoLost = "robot";
            GameManager.instance.EndGame();
        }
    }

    /*  START GAME/ END GAME FUNCTIONS  */

    private void StartingGame()
    {
        gameObject.SetActive(true);
        transform.position = startingPosition;
        currentHealth = playerHealth;
        ResetAnimations();
        UImanager.Instance.UpdateHealthbar(currentHealth.ToString());
        
    }

    private void GameOver()
    {
        gameObject.SetActive(false);

        horizontal = 0;
        rb.linearVelocity = Vector2.zero;

    }

    /*  ANIMATION  */

    private void ResetAnimations()
    {

        isJumping = false;
        isAttacking = false;
    }

    void PlayAnimationState()
    {
        if (currentAnimation == state) return;
        animator.Play(state);
        currentAnimation = state;
    }

    void ChangeAnimationState()
    {
        
    }

    public void NoFight()
    {
        isAttacking = false;
    }
    public void NoHurt()
    {
        isHurt = false;
    }

}