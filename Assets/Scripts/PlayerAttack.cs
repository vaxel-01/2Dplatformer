using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && Player.Instance.isAttacking)
        {
            Enemy2.Instance.Hurt();
            Player.Instance.isAttacking = false;
        }
    }
}
