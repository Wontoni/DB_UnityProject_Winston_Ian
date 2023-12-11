using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBossManager : MonoBehaviour
{
    [SerializeField] private bool bossAlive = true;
    [SerializeField] private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
        GameObject player = GameObject.Find("Player");
        gameManager.SetPlayer(player);
    }

    public void KillBoss()
    {
        bossAlive = false;
    }

    public bool GetBossAlive()
    {
        return bossAlive;
    }
}
