using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinBossEntrance : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
        if (gameManager.GetPumpkinDefeated())
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameManager.DisableSave();
            Destroy(gameObject);
        }
    }
}
