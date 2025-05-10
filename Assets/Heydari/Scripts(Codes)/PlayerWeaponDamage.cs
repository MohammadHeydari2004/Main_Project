using UnityEngine;

public class PlayerWeaponDamage : MonoBehaviour
{
    [SerializeField] private int damage = 35;

    private void OnTriggerEnter2D(Collider2D other)
    {

        Debug.Log("Collision with: " + other.name + " | Tag1: " + other.tag + " | Layer1: " + LayerMask.LayerToName(other.gameObject.layer));

        // برخورد با دشمن Enemy
        if (other.CompareTag("Enemy"))
        {
            HealthEnemy enemyHealth = other.GetComponent<HealthEnemy>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }

        Debug.Log("Collision with: " + other.name + " | Tag2: " + other.tag + " | Layer2: " + LayerMask.LayerToName(other.gameObject.layer));

        // برخورد با باس Boss
        if (other.CompareTag("Boss"))
        {

            BossHealth bossHealth = other.GetComponent<BossHealth>();
            if (bossHealth != null)
            {
                Debug.Log("Boss Detected! Applying Damage...");

                bossHealth.TakeDamageB(damage);
            }
        }
    }
}