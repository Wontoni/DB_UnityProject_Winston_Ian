using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterCave : MonoBehaviour
{
    [SerializeField] public string sceneName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            Vector3 newPos = new(gameObject.transform.position.x, (float)(gameObject.transform.position.y - 0.9), 0);
            gameManager.SetUserPos(newPos);
            gameManager.SavePlayerData();
            gameManager.DisableSave();
            SceneManager.LoadScene(sceneName);
        }
    }
}
