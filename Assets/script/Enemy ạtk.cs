using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public int maxHealth = 3;
    public float detectionRange = 5f;
    public float attackRange = 1.5f;
    public int damage = 3;
    public float attackCooldown = 1f;
    public GameObject player;
    public GameObject enemyPrefab;

    private int currentHealth;
    private bool isAttacking = false;
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 movement;
    public SpriteRenderer spriteRenderer;
    public int XMoveDirection;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;

        // Set initial animation state
        animator.SetBool("idleenemy", true);
        animator.SetBool("runenemy", false);
        animator.ResetTrigger("enemyattack");
        animator.ResetTrigger("enemydie");
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(XMoveDirection, 0));
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= detectionRange && distanceToPlayer > attackRange && currentHealth > 0)
        {
            // Chuyển sang animation chạy
            animator.SetBool("runenemy", true);
            animator.SetBool("idleenemy", false);

            // Di chuyển tới người chơi
            Vector2 direction = (player.transform.position - transform.position).normalized;
            movement = direction;

        }
        else if (distanceToPlayer <= attackRange && !isAttacking && currentHealth > 0)
        {
            // Chuyển sang animation tấn công
            StartCoroutine(Attack());
        }
        else
        {
            // Đứng yên nếu không phát hiện người chơi
            movement = Vector2.zero;
            animator.SetBool("runenemy", false);
            animator.SetBool("idleenemy", true);
        }

    }

    void FixedUpdate()
    {
        if (currentHealth > 0)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
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

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Chạy animation bị tiêu diệt
        animator.SetTrigger("enemydie");

        // Vô hiệu hóa kẻ địch
        rb.velocity = Vector2.zero;
        enabled = false;

        // Hủy đối tượng sau một thời gian ngắn
        Destroy(gameObject, 1f);
        SpawnNewEnemy();
        FindObjectOfType<PlayerController>().AddScore(100);
    }

    private void SpawnNewEnemy()
    {
        // Tạo kẻ địch mới
        Instantiate(enemyPrefab, new Vector2(Random.Range(-8f, 8f), Random.Range(-4f, 4f)), Quaternion.identity);
    }

}