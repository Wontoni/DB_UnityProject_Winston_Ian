using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Vector3 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //mousePos = Input.mousePosition;
        Vector2 worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        Vector2 direction = worldMousePosition - new Vector2(transform.position.x, transform.position.y);
        mousePos = direction.normalized;

        animator.SetFloat("MousePosX", mousePos.x);
        animator.SetFloat("MousePosY", mousePos.y);

    }
}
