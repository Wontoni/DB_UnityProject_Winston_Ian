using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBossManager : MonoBehaviour
{
    [SerializeField] private bool bossAlive = true;

    public void KillBoss()
    {
        bossAlive = false;
    }

    public bool GetBossAlive()
    {
        return bossAlive;
    }
}
