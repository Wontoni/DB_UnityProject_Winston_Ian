using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance { get { return instance; } }

    [SerializeField] private PlayerData userData;

    // Game states
    [SerializeField] private bool gameStarted = false;
    [SerializeField] private bool canSave = true;
    [SerializeField] private float timer = 0;

    // Bosses
    [SerializeField] private bool isSlimeDefeated = false;
    [SerializeField] private bool isPumpkinDefeated = false;

    // Player Object
    [SerializeField] private GameObject playerObj;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        if (gameStarted)
        {
            timer += Time.deltaTime;
        }
    }

    public void StartNewGame()
    {
        timer = 0;
        if (userData != null)
        {
            userData.last_xpos = 0;
            userData.last_ypos = 0;
            userData.is_slime_defeated = false;
            userData.is_pumpkin_defeated = false;
            userData.timer = 0;

            isSlimeDefeated = false;
            isPumpkinDefeated = false;
        }
        EnableSave();
        SavePlayerData();
        ToggleGameStarted(true);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainGame");
    }

    public void ToggleGameStarted(bool state)
    {
        gameStarted = state;
    }

    public void EnableSave()
    {
        canSave = true;
    }

    public void DisableSave()
    {
        canSave = false;
    }

    public void SetPlayer(GameObject player)
    {
        playerObj = player;
    }

    public void SetUserData(PlayerData data)
    {
        if (data != null && data.success)
        {
            userData = data;
            isSlimeDefeated = userData.is_slime_defeated;
            isPumpkinDefeated = userData.is_pumpkin_defeated;
            timer = userData.timer;
        } else
        {
            userData = null;
        }
    }

    public PlayerData GetUserData() 
    { 
        return userData; 
    }

    public Vector3 GetUserPos()
    {
        if (HasPlayerData())
        {
            return new Vector3(userData.last_xpos, userData.last_ypos, 0);
        }
        return Vector3.zero;
    }

    public void SetUserPos(Vector3 pos)
    {
        if (userData != null)
        {
            userData.last_xpos = pos.x;
            userData.last_ypos = pos.y;
        }
    }

    public bool GetSlimeDefeated()
    {
        if (HasPlayerData())
        {
            return isSlimeDefeated;
        }

        return false;
    }

    public void DefeatSlime()
    {
        isSlimeDefeated = true;
        EnableSave();
        SavePlayerData();
    }

    public bool GetPumpkinDefeated()
    {
        if (HasPlayerData())
        {
            return isPumpkinDefeated;
        }

        return false;
    }

    public void DefeatPumpkin()
    {
        isPumpkinDefeated = true;
        EnableSave();
        SavePlayerData();
    }

    public void SavePlayerData()
    {
        if (canSave)
        {
            StartCoroutine(SaveUserData());
        } else
        {
            print("Can't save right now!");
        }
    }

    public bool HasPlayerData()
    {
        if (userData == null) return false;
        return userData.success;
    }

    public IEnumerator SaveUserData()
    {
        WWWForm form = new();
        Vector3 pos = (playerObj == null) ? GetUserPos() : playerObj.transform.position;
        form.AddField("last_xpos", pos.x.ToString());
        form.AddField("last_ypos", pos.y.ToString());
        form.AddField("is_slime_defeated", isSlimeDefeated ? "1" : "0");
        form.AddField("is_pumpkin_defeated", isPumpkinDefeated ? "1" : "0");
        form.AddField("timer", timer.ToString());
        form.AddField("has_save", HasPlayerData() ? "1" : "0");

        string url = "https://unity-backend.onrender.com/save/newSaveData";
        UnityWebRequest request = UnityWebRequest.Post(url, form);
        yield return request.SendWebRequest();
        RequestFormat response = JsonUtility.FromJson<RequestFormat>(request.downloadHandler.text);
        if(!response.success)
        {
            print("Failed to save");
        }
    }

    public void GameOver()
    {
        userData.last_xpos = 0;
        userData.last_ypos = 0;
        isSlimeDefeated = false;
        isPumpkinDefeated = false;
        EnableSave();
        SavePlayerData();
        Destroy(playerObj);

    }

    public bool CheckWin()
    {
        return isPumpkinDefeated && isSlimeDefeated;
    }

    public float TimerCount()
    {
        return timer;
    }
}
