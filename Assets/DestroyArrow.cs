using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyArrow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(DelayDestroy), 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DelayDestroy()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
