using System.Threading;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    [Header("Movement things")]
    [SerializeField] private Rigidbody2D rb;
    
    public float speed=3;
    private float _speed;
    private float distance;
    public float minDistance=0.5f;

    private float pauseMovementTimer;
    public float standStillTime=3;
    private bool isPausing;

    [Header("Path")]
    public Transform pointL;
    public Transform pointR;
    private Transform currentPoint;

    [Header("Animation stuff")]
    [SerializeField] private Animator anim;
    private string currentAnimation;
    private string state;

    private const string IDLE = "Enemy2_Idle";
    private const string WALK = "Enemy2_Walk";

    private EnemyState enemyState;

    [Header("Shooting")]
    public GameObject bullet;
    public Transform bulletPos;

    private float shootCooldownTimer;
    public float shootCooldown;
    public GameObject player;
    public float reachDistance;

    //Lägga till attack och skjuta

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.position = new Vector2(pointL.position.x, rb.position.y);
        _speed = speed;
        distance = 0;
        currentPoint = pointR;
        isPausing = false;
        pauseMovementTimer = 0;
        state= IDLE;
        enemyState = EnemyState.Patrol;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isPlaying)
        {

            switch (enemyState)
            {
                case EnemyState.Patrol:
                    EnemyWalking();
                    break;
                case EnemyState.Fight:
                    EnemyShooting();
                    break;
                case EnemyState.Die:
                    break;
                default:
                    enemyState = EnemyState.Patrol;
                    break;
            }

        }
        ChangeAnimationState();
    }

    private void EnemyShooting()
    {
        shootCooldownTimer += Time.deltaTime;
        if (shootCooldownTimer > shootCooldown)
        {
            shootCooldownTimer = 0;
            Shoot();
        }

        float playerDistance = Vector2.Distance(transform.position, player.transform.position);

        if (playerDistance > reachDistance)
        {
            enemyState= EnemyState.Patrol;
        }
    }

    private void Shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }

    private void EnemyWalking()
    {
        if (!isPausing)
        {
            state = WALK;
            rb.linearVelocity = new Vector2(_speed, rb.position.y);
            distance = Vector2.Distance(transform.position, currentPoint.position);
            if (distance < minDistance)
            {
                isPausing = true;
                rb.linearVelocity = Vector2.zero;
            }
        }
        else
        {
            state = IDLE;
            pauseMovementTimer += Time.deltaTime;
            if (isPausing && pauseMovementTimer >= standStillTime)
            {
                pauseMovementTimer = 0;
                isPausing = false;
                Flip();
            }
        }
        float playerDistance = Vector2.Distance(transform.position, player.transform.position);

        if (playerDistance < reachDistance)
        {
            enemyState = EnemyState.Fight;
            rb.linearVelocity = Vector2.zero;
        }
    }


    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;

        if (currentPoint == pointL) currentPoint = pointR;
        else currentPoint = pointL;

        _speed = -_speed;
    }


    /* ANIMATION */
    void ChangeAnimationState()
    {
        if (currentAnimation == state) return;
        anim.Play(state);
        currentAnimation = state;
    }
}

