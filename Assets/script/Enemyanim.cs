using UnityEngine;

public class EnemyAnimationControl : MonoBehaviour
{
    public Animator animator;
    public Transform player;
    public float detectionRange = 5f;

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance < detectionRange)
        {
            animator.SetBool("runenemy", true);
        }
        else
        {
            animator.SetBool("runenemy", false);
        }
    }
}