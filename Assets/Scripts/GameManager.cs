using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    //GameManager.instance
    #region
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    [Header("Game Management")]
    public UnityEvent onGamePlay = new UnityEvent();
    public UnityEvent onGameEnd = new UnityEvent();
    public bool isPlaying = false;
    public bool isPaused = false; //temp - for fun :)
    public string whoLost = "no one";


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isPlaying)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartGame();
            }
        }
    }

    /* GAME STATE FUNCTIONS */

    public void StartGame()
    {
        onGamePlay.Invoke();
        isPlaying = true;
        whoLost = "no one";
        UImanager.Instance.HideScreen(UImanager.Instance.gameOverScreenA);
        UImanager.Instance.HideScreen(UImanager.Instance.gameOverScreenB);

    }

    public void EndGame()
    {
        onGameEnd.Invoke();
        isPlaying=false;
        
        

        switch (whoLost)
        {
            case "robot":
                UImanager.Instance.ShowScreen(UImanager.Instance.gameOverScreenA);
                break;
            case "science":
                UImanager.Instance.ShowScreen(UImanager.Instance.gameOverScreenB);
                break;
            default: //Vet spelet inte vem det var som vann/förlorade, förlorar spelaren
                UImanager.Instance.ShowScreen(UImanager.Instance.gameOverScreenA);
                break;
        }
    }
}
