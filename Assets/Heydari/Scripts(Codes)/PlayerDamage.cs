using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField] private float damage = 20f; // دمیج هر حمله
    [SerializeField] private float attackRange = 1.5f; // شعاع حمله
    [SerializeField] private LayerMask enemyLayer; // لایه دشمنان

    public void OnAttack()
    {
        // جستجوی دشمنان در شعاع حمله
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            HealthEnemyNormal enemyHealth = enemy.GetComponent<HealthEnemyNormal>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }
    }

    // رسم دایره حمله در Scene View برای دیباگ
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
