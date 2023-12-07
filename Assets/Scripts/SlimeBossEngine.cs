using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBoss : MonoBehaviour
{
    [SerializeField] GameObject slimeboss;

    [Range(0f, 20f)]
    [SerializeField] float period = 1.0f;

    [SerializeField] float moveSpeed = 5f;
    Rigidbody2D rb;
    Transform target;
    Vector2 moveDirection;

    private float nextActionTime = 0.0f;
    Animator animator;

    PolygonCollider2D hitbox;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        hitbox = GetComponent<PolygonCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time > nextActionTime)
        {
            nextActionTime += period;
            Jump(hitbox);
            //gotHit();
        }

        chasePlayer();

        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("Green Jump - Jump Land"))
        {
            hitbox.GetComponent<PolygonCollider2D>().enabled = true;
        }

    }

    private void FixedUpdate()
    {
        if (target)
        {
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
        }
        
    }

    private void Jump(PolygonCollider2D other)
    {
        animator.SetTrigger("isJumping");
        other.GetComponent<PolygonCollider2D>().enabled = false;
    }

    private void gotHit()
    {
        animator.SetTrigger("isHurt");
        transform.localScale = new Vector3(transform.localScale.x - 1f, transform.localScale.y - 1f); // max 5 times
    }

    private void SlimeDuplicate()
    {
        GameObject newSlimeBoss = Instantiate(slimeboss);
        newSlimeBoss.transform.localScale = new Vector3(transform.localScale.x - 1f, transform.localScale.y - 1f);
        newSlimeBoss.transform.position = transform.position;
        newSlimeBoss.SetActive(true);
    }

    private void chasePlayer()
    {
        if (target)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            moveDirection = direction;
        }
    }
}

