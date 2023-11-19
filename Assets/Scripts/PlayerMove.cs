using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Vector2 moveInput;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] public float speed;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();

        animator.SetFloat("Horizontal", moveInput.x);
        animator.SetFloat("Vertical", moveInput.y);
        animator.SetFloat("Speed", moveInput.sqrMagnitude);

        if (Input.GetKeyUp(KeyCode.W))
        {
            animator.SetFloat("lastVertical", 1);
            animator.SetFloat("lastHorizontal", 0);
        } 
        else if(Input.GetKeyUp(KeyCode.S))
        {
            animator.SetFloat("lastVertical", -1);
            animator.SetFloat("lastHorizontal", 0);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            animator.SetFloat("lastVertical", 0);
            animator.SetFloat("lastHorizontal", 1);
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            animator.SetFloat("lastVertical", 0);
            animator.SetFloat("lastHorizontal", -1);
        }

    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput * speed * Time.fixedDeltaTime);
    }
}
