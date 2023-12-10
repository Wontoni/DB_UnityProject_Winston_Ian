using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameManager : MonoBehaviour
{
    [SerializeField] public GameObject gameManager;
    [SerializeField] public GameManager manager;

    [SerializeField] public GameObject player;

    [SerializeField] public GameObject pausePopup;
    [SerializeField] public GameObject settings;

    [SerializeField] public GameObject gameOverCanvas;

    [SerializeField] public bool isBossRoom = false;

    private void Awake()
    {
        settings = GameObject.Find("Settings");
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        if (gameManager == null)
        {
            SceneManager.LoadScene("TitleScreen");
        } else if(!isBossRoom)
        {
            manager = gameManager.GetComponent<GameManager>();
            player.transform.position = manager.GetUserPos();
            manager.SetPlayer(player);
        } else
        {
            manager = gameManager.GetComponent<GameManager>();
        }
    }

    private void Update()
    {
        if (pausePopup.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            pausePopup.SetActive(false);
            Time.timeScale = 1f;
            manager.ToggleGameStarted();
        }

        if(player == null)
        {
            Time.timeScale = 0f;
            settings.SetActive(false);
            pausePopup.SetActive(false);
            gameOverCanvas.SetActive(true);
        }
    }

    public void PauseGame()
    {
        if (!pausePopup.activeSelf)
        {
            pausePopup.SetActive(true);
            Time.timeScale = 0f;
            manager.ToggleGameStarted();
        } else 
        {
            pausePopup.SetActive(false);
            Time.timeScale = 1f;
            manager.ToggleGameStarted();
        }
    }

    public void ResumeGame()
    {
        pausePopup.SetActive(false);
        Time.timeScale = 1.0f;
        manager.ToggleGameStarted();
    }

    public void ExitGame()
    {
        Time.timeScale = 0.0f;
        SceneManager.LoadScene("MainScreen");
    }

    public void SaveGame()
    {
        manager.SavePlayerData();
    }

    public void GameOverNewGame()
    {
        manager.StartNewGame();
    }
}
