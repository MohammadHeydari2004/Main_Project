using System.Collections;
using UnityEngine;

public class AttackEnemy : MonoBehaviour
{
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private LayerMask playerLayerMask;
    [SerializeField] private Collider2D enemyWeaponCollider; // تنظیم در اینسپکتور
    [SerializeField] private float attackCooldown = 1.5f;
    [SerializeField] private float attackDuration = 0.2f;

    private float lastAttackTime;

    private void Start()
    {
        lastAttackTime = Time.time;
    }

    private void Update()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, attackRange, playerLayerMask);
        if (player != null && Time.time >= lastAttackTime + attackCooldown)
        {
            ActivateEnemyWeaponCollider();
            lastAttackTime = Time.time;
        }
    }

    public void ActivateEnemyWeaponCollider()
    {
        enemyWeaponCollider.enabled = true;
        StartCoroutine(DisableColliderAfterDelay());
    }

    private IEnumerator DisableColliderAfterDelay()
    {
        yield return new WaitForSeconds(attackDuration);
        enemyWeaponCollider.enabled = false;
    }
}