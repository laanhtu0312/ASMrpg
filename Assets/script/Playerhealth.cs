using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 50;
    public int currentHealth;
    public Slider healthSlider;
    

    private void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        
        // Chạy animation bị hạ gục
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("died");

        // Vô hiệu hóa điều khiển nhân vật
        GetComponent<PlayerController>().enabled = false;


    }
}