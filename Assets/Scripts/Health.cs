using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private bool dead;


    private void Awake()
    {
        currentHealth=startingHealth;
        dead=false;
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);

        if(currentHealth > 0)
        {

        }
        else
        {
            if (!dead)
            {
                dead = true;
                GameManager.instance.EndGame();
            }
        }
    }

    
}
