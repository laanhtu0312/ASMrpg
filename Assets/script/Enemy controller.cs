using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyAI : MonoBehaviour
{
    public float speed = 2f;
    public float attackRange = 1.5f;
    public float detectionRange = 5f;
    public int damage = 3;
    public int enemyHealth = 3;

    private Transform player;
    private Animator animator;
    private bool isFacingRight = true;
    private bool isPlayerDetected = false;
    private bool isAttacking = false;
    public float moveSpeed = 3f;
    public int maxHealth = 3;

    public float attackCooldown = 1f;
    public GameObject enemyPrefab;
    private int currentHealth;
    private Rigidbody2D rb;
    private Vector2 movement;
    public SpriteRenderer spriteRenderer;
    public int XMoveDirection;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;

        animator.SetBool("idleenemy", true);
        animator.SetBool("runenemy", false);
        animator.ResetTrigger("enemyattack");
        animator.ResetTrigger("enemydie");
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            isPlayerDetected = true;
        }
        else
        {
            isPlayerDetected = false;
        }

        if (isPlayerDetected)
        {
            if (distanceToPlayer > attackRange)
            {
                MoveTowardsPlayer();
            }
            else
            {
                AttackPlayer();
            }
        }
        else if (distanceToPlayer <= attackRange && !isAttacking && currentHealth > 0)
        {
            // Chuyển sang animation tấn công
            StartCoroutine(Attack());
        }
        else
        {
            animator.SetBool("runenemy", false);
        }

    }

    void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        if (direction.x > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (direction.x < 0 && isFacingRight)
        {
            Flip();
        }

        animator.SetBool("runenemy", true);
    }

    void AttackPlayer()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            animator.SetTrigger("enemyattack");
            // Deal damage to the player
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;

        if (enemyHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetTrigger("enemydie");
        Destroy(gameObject, 1f);
        FindObjectOfType<PlayerController>().AddScore(100);
    }
    private IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetTrigger("enemyattack");

        yield return new WaitForSeconds(attackCooldown);

        if (Vector2.Distance(transform.position, player.transform.position) <= attackRange)
        {
            player.GetComponent<PlayerController>().TakeDamage(damage);
        }

        isAttacking = false;
    }



    private void Died()
    {
        // Chạy animation bị tiêu diệt
        animator.SetTrigger("enemydie");

        // Vô hiệu hóa kẻ địch
        rb.velocity = Vector2.zero;
        enabled = false;

        // Hủy đối tượng sau một thời gian ngắn
        Destroy(gameObject, 1f);
        FindObjectOfType<PlayerController>().AddScore(100);

    }
   
}