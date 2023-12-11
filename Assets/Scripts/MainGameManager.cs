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
            //manager = gameManager.GetComponent<GameManager>();
            manager = GameManager.Instance;
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
            manager.ToggleGameStarted(true);
        }

        if(player == null)
        {
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
            manager.ToggleGameStarted(false);
        } else 
        {
            pausePopup.SetActive(false);
            Time.timeScale = 1f;
            manager.ToggleGameStarted(true);
        }
    }

    public void ResumeGame()
    {
        pausePopup.SetActive(false);
        Time.timeScale = 1.0f;
        manager.ToggleGameStarted(true);
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
        Time.timeScale = 1.0f;
        manager.ToggleGameStarted(true);
        manager.EnableSave();
        manager.StartNewGame();
    }
}
