using UnityEngine;

public class UImanager : MonoBehaviour
{
    #region

    public static UImanager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    #endregion

    //public GameObject[] healthbar;
    public GameObject healthbar;
    public GameObject[] currentHealth;
    public float healthbarDistance;
    public float healthbarFrameSize;
    public Transform startPosition;

    private void Start()
    {
        GameManager.instance.onGamePlay.AddListener(CreateHealthbar);
    }

    private void CreateHealthbar()
    {
        for (int i = 0; i < currentHealth.Length; i++)
        {
            currentHealth[i].SetActive(true);
        }
    }

    public void UpdateHealthbar()
    {
        int playerHealth = Player.Instance.currentHealth;

        for(int i =0; i < currentHealth.Length; i++)
        {
            currentHealth[i].SetActive(false);
        }
        for(int i = 0; i<playerHealth; i++)
        {
            currentHealth[i].SetActive(true);
        }
    }
}
