using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScreenManager : MonoBehaviour
{

    [SerializeField] public GameObject gameManager;
    [SerializeField] public GameManager manager;
    [SerializeField] public Button resumeButton;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        StartCoroutine(GetUserData());
    }

    // Start is called before the first frame update
    void Start()
    {
        if (gameManager == null)
        {
            SceneManager.LoadScene("TitleScreen");
        }
        else
        {
            manager = gameManager.GetComponent<GameManager>();
            resumeButton.interactable = manager.HasPlayerData();
        }
    }

    public void NewGame()
    {
        manager.StartNewGame();
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainGame");
    }

    public void ResumeGame()
    {
        manager.ToggleGameStarted();
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainGame");
    }

    public void Logout()
    {
        StartCoroutine(DestroySession());
    }

    public IEnumerator DestroySession()
    {
        WWWForm form = new();
        string url = "http://localhost:3000/auth/logout";
        UnityWebRequest request = UnityWebRequest.Post(url, form);
        yield return request.SendWebRequest();
        var response = request.downloadHandler.text;
        SceneManager.LoadScene("TitleScreen");
    }

    public IEnumerator GetUserData()
    {
        WWWForm form = new();
        string url = "http://localhost:3000/save/saveData";
        UnityWebRequest request = UnityWebRequest.Post(url, form);
        yield return request.SendWebRequest();
        PlayerData playerData = JsonUtility.FromJson<PlayerData>(request.downloadHandler.text);
        gameManager.GetComponent<GameManager>().SetUserData(playerData);
    }

}
