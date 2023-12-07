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

    [SerializeField] public bool isBossRoom = false;

    private void Awake()
    {
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
        SceneManager.LoadScene("MainScreen");
    }

    public void SaveGame()
    {
        manager.SavePlayerData();
    }
}
