using UnityEngine;

public class EnemyWeaponDamage : MonoBehaviour
{
    [SerializeField] private int damage = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HealthPlayer health = other.GetComponent<HealthPlayer>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }
    }
}