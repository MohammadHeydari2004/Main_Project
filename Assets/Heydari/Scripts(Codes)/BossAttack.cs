using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField] private Collider2D enemyWeaponCollider;

    public void ActivateEnemyWeaponCollider()
    {
        enemyWeaponCollider.enabled = true;
    }

    public void DeactivateEnemyWeaponCollider()
    {
        enemyWeaponCollider.enabled = false;
    }
}