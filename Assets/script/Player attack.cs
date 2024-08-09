using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int attackDamage = 3; // Damage dealt by the player to the enemy (can be higher or different depending on game design)
    public float attackRange = 1f; // Range in which the player can hit enemies
    public Transform attackPoint; // The point from where the attack originates
    public LayerMask enemyLayers; // Layers that represent the enemies

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Check if the attack button is pressed (e.g., spacebar)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    void Attack()
    {
        // Play the attack animation
        animator.SetTrigger("attack");

        // Detect enemies in range of the attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Damage each enemy hit
        foreach (Collider2D enemy in hitEnemies)
        {
            // Assuming the enemy has a script with a method TakeDamage(int damage)
            enemy.GetComponent<EnemyAI>().TakeDamage(attackDamage);
        }
    }

    // Optional: Visualize the attack range in the Unity editor
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}