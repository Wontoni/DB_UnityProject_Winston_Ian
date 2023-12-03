using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class FakeHeightObject : MonoBehaviour
{
    [SerializeField] GameObject slimeboss;

    public Transform trnsObject;
    public Transform trnsBody;
    public Transform trnsShadow;

    Animator trnsBodyAnimator;

    public float gravity = -7;
    public Vector2 groundVelocity;
    public float verticalVelocity;

    private int hitsTaken = 5;

    public bool isGrounded;
    private bool jumpStarted = false;

    [Range(0f, 20f)]
    [SerializeField] float period = 1.0f;

    [SerializeField] float moveSpeed = 5f;
    Rigidbody2D rb;
    Transform target;
    Vector2 moveDirection;

    private float nextActionTime = 0.0f;
    private int timeSinceJump = 0;

    PolygonCollider2D hitbox;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        hitbox = GetComponent<PolygonCollider2D>();
        trnsBodyAnimator = trnsBody.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
        CheckGroundHit();
        chasePlayer();

        if (Time.time > nextActionTime)
        {
            nextActionTime += period;
            JumpAnimation();
            jumpStarted = true;
        }

        if (jumpStarted)
        {
            timeSinceJump++;
        }

        if (timeSinceJump == 470) 
        {
            timeSinceJump = 0;
            jumpStarted = false;
            Initialize(rb.velocity, 13);
            hitbox.enabled = false;
        }

        if (trnsBodyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Green Death - Animation"))
        {
            Destroy(gameObject);
        }
    }

    void UpdatePosition()
    {
        if (!isGrounded)
        {
            verticalVelocity += gravity * Time.deltaTime;
            trnsBody.position += new Vector3(0, verticalVelocity, 0) * Time.deltaTime;
            if (verticalVelocity <= -11)
            {
                DescendAnimation();
            }
        }

        trnsObject.position += (Vector3)groundVelocity * Time.deltaTime;
    }

    public void Initialize(Vector2 groundVelocity, float verticalVelocity)
    {
        isGrounded = false;
        this.groundVelocity = groundVelocity;
        this.verticalVelocity = verticalVelocity;
    }

    void CheckGroundHit()
    {
        if (trnsBody.position.y < trnsObject.position.y && !isGrounded)
        {
            trnsBody.position = trnsObject.position;
            isGrounded = true;
            GroundHit();
        }
    }

    void GroundHit()
    {
        hitbox.enabled = true;
    }

    private void FixedUpdate()
    {
        if (target)
        {
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
        }
    }

    private void chasePlayer()
    {
        if (target)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            moveDirection = direction;
        }
    }

    private void JumpAnimation()
    {
        trnsBodyAnimator.SetTrigger("isJumping");
    }

    private void DescendAnimation()
    {
        trnsBodyAnimator.SetTrigger("isFalling");
    }

    private void gotHit()
    {
        hitsTaken--;

        if (hitsTaken <= 0)
        {
            trnsBodyAnimator.SetTrigger("isHurt");
            trnsBodyAnimator.SetTrigger("isDead");
        } else
        {
            trnsBodyAnimator.SetTrigger("isHurt");

            trnsBody.transform.localScale = new Vector3(trnsBody.transform.localScale.x - 0.75f, trnsBody.transform.localScale.y - 1f); // max 5 times

            trnsShadow.transform.localScale = new Vector3(trnsShadow.transform.localScale.x - 0.72f, trnsShadow.transform.localScale.y);
            trnsShadow.transform.position = new Vector3(trnsShadow.transform.position.x, trnsShadow.transform.position.y + 0.5f);
        }
    }

    private void SlimeDuplicate()
    {
        GameObject newSlimeBoss = Instantiate(slimeboss) as GameObject;
        newSlimeBoss.transform.position = new Vector2(-1, 4);
        newSlimeBoss.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            gotHit();
            SlimeDuplicate();
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player is dead");
        }
    }

}
