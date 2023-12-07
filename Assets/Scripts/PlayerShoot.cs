using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Boolean canShoot;
    [SerializeField] public GameObject arrowPrefab;
    [SerializeField] public float shootForce = 5f;

    // Start is called before the first frame update
    void Start()
    {
        canShoot = false;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("isDrawBow", true);
        } else if (Input.GetMouseButtonUp(0))
        {
            animator.SetBool("isDrawBow", false);

            if(canShoot)
            {
                ShootArrow();
            }
        }
    }

    public void AllowShoot()
    {
        canShoot = true;
    }

    public void ShootArrow()
    {
        Vector3 targetDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        GameObject arrow = Instantiate(arrowPrefab);
        arrow.transform.position = transform.position;
        Rigidbody2D arrowRb = arrow.GetComponent<Rigidbody2D>();
        float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
        arrowRb.rotation = angle;

        arrowRb.AddForce((Vector2) targetDir * shootForce, ForceMode2D.Impulse);
        canShoot = false;
        animator.SetBool("isDrawBow", false);
    }
}
