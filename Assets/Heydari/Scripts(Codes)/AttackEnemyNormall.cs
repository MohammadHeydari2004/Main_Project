using UnityEngine;

public class AttackEnemyNormall : MonoBehaviour
{
    public float damage = 20f;
    public float attackCooldown = 1f; // زمان بین هر حمله
    private float lastAttackTime;

    void Update()
    {
        // مثال: فشردن دکمه برای حمله
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > lastAttackTime + attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    void Attack()
    {
        // تشخیص دشمنان در محدوده (مثلاً با Raycast یا Trigger)
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, 2f, LayerMask.GetMask("Enemy"));

        foreach (Collider enemy in hitEnemies)
        {
            Health_Player enemyHealth = enemy.GetComponent<Health_Player>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }
    }

    // یا استفاده از OnCollisionEnter برای برخورد مستقیم
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Health_Player enemyHealth = collision.gameObject.GetComponent<Health_Player>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }
    }
}
