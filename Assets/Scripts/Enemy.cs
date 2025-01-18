using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Enemy : MonoBehaviour
{

    [SerializeField] private Animator animator;

    [SerializeField] private int health = 3;
    [SerializeField] private bool carriesKey;

    [SerializeField] private GameObject carriedKey;

    private void Start()
    {
        animator.ResetTrigger("die");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && Player.Instance.currentAnimation== "Player_Attack")
        {
            health--;
            if (health == 0)
            {
                animator.SetTrigger("die");
                
            }
        }
    }
}
