using UnityEngine;

public class HealthPlayer : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private Animator animator; // تنظیم در اینسپکتور

    private int currentHealth;
    private bool isDead = false;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        if (isDead) return;

        Debug.Log("health P: " + currentHealth);
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // در تابع TakeDamage، برخورد با حمله باس را اضافه کنید:
    public void TakeDamageFromBoss(int damage)
    {
        if (isDead) return;
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;

        // غیرفعال‌کردن مولفه‌های دیگر
        GetComponent<Movement_Player>().enabled = false;
        GetComponent<Attack_Player>().enabled = false;
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<Collider2D>().enabled = false;

        // پخش انیمیشن مرگ
        animator.SetTrigger("Die");
    }

    // این متد در Animation Event فراخوانی می‌شود
    public void OnDeathAnimationEnd()
    {
        Destroy(gameObject);
    }
}