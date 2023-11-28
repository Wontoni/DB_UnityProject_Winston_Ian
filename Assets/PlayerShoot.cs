using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
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
        }
    }

    public void ShootArrow()
    {
        print("SHOOT ARROW");
    }
}
