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

    }

    public void EndGame()
    {
        onGameEnd.Invoke();
        isPlaying=false;
    }
}
