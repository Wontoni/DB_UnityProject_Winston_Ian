using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameManager manager;
    [SerializeField] GameObject gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        if (gameManager == null)
        {
            SceneManager.LoadScene("TitleScreen");
        } else
        {
            manager = gameManager.GetComponent<GameManager>();
            manager.SetPlayer(gameObject);

            gameObject.transform.position = manager.GetUserPos();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
