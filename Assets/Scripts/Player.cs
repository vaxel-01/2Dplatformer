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


    [SerializeField] private const string IDLE = "Player_Idle2";
    [SerializeField] private const string RUN = "Player_Run2";
    [SerializeField] private const string JUMP = "Player_Jump2";
    [SerializeField] private const string ATTACK = "Player_Attack";
    //[SerializeField] private const string DIE = "";

    [SerializeField] private int health;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.instance.onGamePlay.AddListener(StartingGame);
        GameManager.instance.onGameEnd.AddListener(GameOver);
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isPlaying)
        {
            horizontal = Input.GetAxis("Horizontal");
            

            if(horizontal !=0 && !isJumping && !isAttacking)
            {
                state = "run";
            }
            
            if (Input.GetButtonDown("Jump") && IsGrounded())
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);

                isJumping = true;

            }
            if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
            }
            if(IsGrounded() && isJumping)
            {
                isJumping = false;
            }
            if (rb.linearVelocityY != 0)
            {
                state = "jump";
            }


            if (Input.GetButtonDown("Fire1") && !isAttacking)
            {
                isAttacking = true;
                state = "attack";
            }

            if(IsGrounded() && !isJumping && !isAttacking && horizontal == 0)
            {
                state = "idle";
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




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Key")
        {
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy") //Get hurt
        {

        }
    }

    private void StartingGame()
    {
        gameObject.SetActive(true);
        transform.position = startingPosition;
        ResetAnimations();
        
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
        switch (state)
        {
            case "idle":
                state = IDLE;
                break;
            case "run":
                state = RUN;
                break;
            case "jump":
                state = JUMP;
                break;
            case "attack":
                state = ATTACK;
                break;
            default:
                break;
        }
        ChangeAnimationState();
    }

    void ChangeAnimationState()
    {
        if (currentAnimation == state) return;
        animator.Play(state);
        currentAnimation = state;
    }

    public void StopFighting()
    {
        isAttacking = false;
    }
}