using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private Vector2 moveInput;
    [SerializeField] private Vector2 dodgeInput;

    [SerializeField] public float speed = 4.0f;
    [SerializeField] public float dodgeSpeed = 7.0f;
    [SerializeField] public float dodgeCooldownCounter = 0.0f;
    [SerializeField] private float dodgeCoolodwnTotal = 0.3f;
    [SerializeField] private bool isDodging = false;

    [SerializeField] public ParticleSystem dust;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
        if (dodgeCooldownCounter >= 0)
        {
            dodgeCooldownCounter -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void GetInputs()
    {
        if (!isDodging)
        {
            if (Input.GetMouseButton(0))
            {
                moveInput *= 0;
            }
            else
            {
                moveInput.x = Input.GetAxisRaw("Horizontal");
                moveInput.y = Input.GetAxisRaw("Vertical");
                moveInput.Normalize();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isDodging && dodgeCooldownCounter < 0) 
        {
            boxCollider.enabled = false;
            isDodging = true;
            animator.SetBool("isDodgeRoll", true);
            dust.Play();
        }
        animator.SetFloat("MoveSpeed", moveInput.sqrMagnitude);
        if (moveInput.sqrMagnitude == 0)
        {
            Vector2 worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            Vector2 direction = worldMousePosition - new Vector2(transform.position.x, transform.position.y);
            dodgeInput = direction.normalized;
        } 
        else
        {
            dodgeInput = moveInput;
        }
        animator.SetFloat("MoveX", dodgeInput.x * 2);
        animator.SetFloat("MoveY", dodgeInput.y * 2);
    }

    private void MovePlayer()
    {
        if (!isDodging)
        {
            rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * moveInput);
        } else
        {
            rb.MovePosition(rb.position + dodgeSpeed * Time.fixedDeltaTime * dodgeInput);
        }
        if (rb.velocity.sqrMagnitude > 0)
        {
            dust.Play();
        }
    }

    public void AllowDodge()
    {
        isDodging = false;
        animator.SetBool("isDodgeRoll", false);
        dodgeCooldownCounter = dodgeCoolodwnTotal;
        boxCollider.enabled = true;
    }
}
