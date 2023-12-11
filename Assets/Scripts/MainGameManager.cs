using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameManager : MonoBehaviour
{
    [SerializeField] public GameManager gameManager;

    [SerializeField] public GameObject player;

    [SerializeField] public GameObject pausePopup;
    [SerializeField] public GameObject settings;

    [SerializeField] public GameObject gameOverCanvas;
    [SerializeField] public GameObject gameWinCanvas;
    [SerializeField] public TMP_Text gameWinTimeText;

    [SerializeField] public bool isBossRoom = false;

    private void Awake()
    {
        settings = GameObject.Find("Settings");
        gameManager = GameManager.Instance;
        if (gameManager == null)
        {
            SceneManager.LoadScene("TitleScreen");
        } else if(!isBossRoom)
        {
            //manager = gameManager.GetComponent<GameManager>();
            gameManager = GameManager.Instance;
            player.transform.position = gameManager.GetUserPos();
            gameManager.SetPlayer(player);
        } else
        {
            gameManager = gameManager.GetComponent<GameManager>();
        }
    }

    private void Start()
    {
        WinGame();
    }

    private void Update()
    {
        if (pausePopup.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            pausePopup.SetActive(false);
            Time.timeScale = 1f;
            gameManager.ToggleGameStarted(true);
        }

        if(player == null)
        {
            settings.SetActive(false);
            pausePopup.SetActive(false);
            gameWinCanvas.SetActive(false);
            gameOverCanvas.SetActive(true);
        }
    }

    public void PauseGame()
    {
        if (!pausePopup.activeSelf)
        {
            pausePopup.SetActive(true);
            Time.timeScale = 0f;
            gameManager.ToggleGameStarted(false);
        } else 
        {
            pausePopup.SetActive(false);
            Time.timeScale = 1f;
            gameManager.ToggleGameStarted(true);
        }
    }

    public void ResumeGame()
    {
        pausePopup.SetActive(false);
        Time.timeScale = 1.0f;
        gameManager.ToggleGameStarted(true);
    }

    public void ExitGame()
    {
        Time.timeScale = 0.0f;
        SceneManager.LoadScene("MainScreen");
    }

    public void SaveGame()
    {
        gameManager.SavePlayerData();
    }

    public void GameOverNewGame()
    {
        Time.timeScale = 1.0f;
        gameWinCanvas.SetActive(false);
        gameManager.ToggleGameStarted(true);
        gameManager.EnableSave();
        gameManager.StartNewGame();
    }

    public void WinGame()
    {
        if (gameManager.CheckWin())
        {
            player.transform.position = Vector3.zero;
            int seconds = (int) gameManager.TimerCount() % 60;
            int minutes = (int) gameManager.TimerCount() / 60;
            gameWinTimeText.text = minutes + "m " + seconds + "s";
            Time.timeScale = 0;
            settings.SetActive(false);
            pausePopup.SetActive(false);
            gameOverCanvas.SetActive(false);
            gameWinCanvas.SetActive(true);
        }
    }
}
