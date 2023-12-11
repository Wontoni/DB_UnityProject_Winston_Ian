using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaveCave : MonoBehaviour
{
    [SerializeField] public string sceneName;

    BoxCollider2D hitbox;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager gameManager = GameManager.Instance;
            gameManager.EnableSave();
            SceneManager.LoadScene(sceneName);
        }
    }

    private void Awake()
    {
        hitbox = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        hitbox.enabled = false;
    }

    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("SlimeBoss").Length == 0)
        {
            hitbox.enabled = true;
        }
    }
}
