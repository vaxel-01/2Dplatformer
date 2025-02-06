using TMPro;
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

    public TextMeshProUGUI healthText;
    public GameObject gameOverScreenA;
    public GameObject gameOverScreenB;

    public void UpdateHealthbar(string health)
    {
        healthText.text = health;
    }
    
    public void ShowScreen(GameObject screen)
    {
        screen.SetActive(true);
    }
    public void HideScreen(GameObject screen)
    {
        screen.SetActive(false);
    }

}
