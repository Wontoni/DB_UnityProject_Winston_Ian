using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinBoss : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject player;
    [SerializeField] private bool isIdle = true;
    [SerializeField] private bool isAttacking = false;
    [SerializeField] private float attackCoolDownCounter = 0.0f;
    [SerializeField] private float attackCoolDown = 3.0f;
    [SerializeField] private float speed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isIdle && attackCoolDownCounter > 0.0f)
        {
            attackCoolDownCounter -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (!isIdle && !isAttacking)
        {
            MoveToPlayer();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isIdle && (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Projectile")))
        {
            isIdle = false;
        } else if (collision.gameObject.CompareTag("Player"))
        {
            gameManager.GameOver();
        }
        rb.velocity = Vector3.zero;
    }

    private void MoveToPlayer()
    {
        if (player != null)
        {
            Vector2 playerDir = player.transform.position - transform.position;
            if (playerDir.x < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }


            if (playerDir.magnitude < 10 && attackCoolDownCounter < 0.0f)
            {
                Attack();
            } else { 
            }

            rb.velocity = speed * playerDir.normalized;
        } else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void Attack()
    {
        animator.SetBool("isAttack", true);
        isAttacking = true;
    }

    public void EndAttack()
    {
        isAttacking = false;
        animator.SetBool("isAttack", false);
        attackCoolDownCounter = attackCoolDown;
    }
}
