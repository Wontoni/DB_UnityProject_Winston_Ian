using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class FakeHeightObject : MonoBehaviour
{
    [SerializeField] GameObject slimeboss;

    [SerializeField] SlimeBossManager slimeBossManager;

    public Transform trnsObject;
    public Transform trnsBody;
    public Transform trnsShadow;

    Animator trnsBodyAnimator;

    public float gravity = -7;
    public Vector2 groundVelocity;
    public float verticalVelocity;

    private int hitsTaken = 3;

    public bool isGrounded;
    private bool jumpStarted = false;

    [Range(0f, 20f)]
    [SerializeField] float period = 1.0f;

    [SerializeField] float moveSpeed = 4f;
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
        slimeBossManager = GameObject.Find("SlimeBossManager").GetComponent<SlimeBossManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").transform;
        if(gameObject.CompareTag("SlimeDuplicate"))
        {
            hitsTaken = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (slimeBossManager.GetBossAlive())
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
        } else
        {
            killSlime();
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

        trnsBodyAnimator.SetTrigger("isHurt");
        if (hitsTaken <= 0) { 
       
            trnsBodyAnimator.SetTrigger("isDead");
        } else
        {

            trnsBody.transform.localScale = new Vector3(trnsBody.transform.localScale.x - 0.75f, trnsBody.transform.localScale.y - 1f); // max 5 times

            trnsShadow.transform.localScale = new Vector3(trnsShadow.transform.localScale.x - 0.72f, trnsShadow.transform.localScale.y);
            trnsShadow.transform.position = new Vector3(trnsShadow.transform.position.x, trnsShadow.transform.position.y + 0.5f);
        }
    }

    public void killSlime()
    {
        trnsBodyAnimator.SetTrigger("isDead");
    }

    private void SlimeDuplicate()
    {
        GameObject newSlimeBoss = Instantiate(slimeboss);
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

    private void OnDestroy()
    {
        GameObject[] slimes = GameObject.FindGameObjectsWithTag("SlimeDuplicate");

        slimeBossManager.KillBoss();
        foreach (GameObject item in slimes)
        {
            item.GetComponent<FakeHeightObject>().killSlime();
        }

    }

}
