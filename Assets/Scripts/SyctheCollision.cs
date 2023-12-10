using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyctheCollision : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    private void Start()
    {
        GameObject managerObj = GameObject.FindGameObjectWithTag("GameManager");
        if (managerObj != null )
        {
            gameManager = managerObj.GetComponent<GameManager>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            gameManager.GameOver();
        }
    }
}
