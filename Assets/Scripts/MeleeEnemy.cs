using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    private string currentState; //idle -> attack -> idle osv.

    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private int walkingRange = 5;

    [SerializeField] private float speed = 5;
    private float _speed;
    private char target;
    private bool isFacingRight = true;

    [SerializeField] private Transform playerPosition;

    [SerializeField] private Animator animator;

    private float shootTimer;
    [SerializeField] private float shootCooldown;
    [SerializeField] private float shootDistance;





    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _speed = speed;
        transform.position = pointA.position;
        target = 'b';
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case "fight":
                FightState();
                break;
            default: //idle'
                IdleState();
                break;
        }
    }



    private void IdleState()
    {
        transform.position = new Vector3(transform.position.x + _speed, transform.position.y);
        if (target == 'a' && transform.position.x >= pointA.position.x || target == 'b' && transform.position.x >= pointB.position.x)
        {
            Turn();
        }
        if (Mathf.Abs(playerPosition.position.y) == Mathf.Abs(transform.position.y))
        {
            currentState = "fight";
            shootTimer = 0;
        }
    }
    private void Turn()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;

        _speed = -_speed;

        if (target == 'a') target = 'b';
        else target = 'a';
    }



    private void FightState()
    {
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootCooldown)
        {
            if (Mathf.Abs(playerPosition.position.y) == Mathf.Abs(transform.position.y) && playerPosition.position.x <= (transform.position.x + shootDistance) || playerPosition.position.x >= (transform.position.x - shootDistance))
            {
                //shoot;
            }
            else
            {
                shootTimer = 0;
                currentState = "idle";
            }
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
}
