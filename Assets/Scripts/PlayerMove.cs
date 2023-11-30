using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Vector2 moveInput;
    [SerializeField] private Animator animator;

    [SerializeField] public float speed;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetMouseButton(0))
        {
            moveInput *= 0;
        } else {
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");
            moveInput.Normalize();
        }
        animator.SetFloat("MoveSpeed", moveInput.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * moveInput);
    }
}
