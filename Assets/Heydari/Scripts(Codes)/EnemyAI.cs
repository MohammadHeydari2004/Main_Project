using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Animator animator;
    [SerializeField] private AttackEnemy attackEnemy;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float idleToMoveDelay = 0.2f;
    [SerializeField] private float movementDelayAfterIdle = 0.5f; 

    private Rigidbody2D rb;
    private bool isAttacking = false;
    private bool isIdleAnimationFinished = false;
    private bool hasPlayedInitialIdle = false;
    private bool canMove = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (player == null || isAttacking)
            return;

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        bool isInIdleState = stateInfo.IsName("enemy2-idle");

        // غیرفعال کردن حرکت در حالت Idle
        if (isInIdleState && !hasPlayedInitialIdle)
        {
            rb.linearVelocity = Vector2.zero;
            animator.SetFloat("Speed", 0f);
            return;
        }

        // انتظار برای فعال شدن حرکت
        if (!isIdleAnimationFinished)
            return;

        // اجرای حرکت فقط بعد از تمام شدن تاخیر
        if (!canMove)
            return;

        Vector2 direction = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // حرکت به سمت پلیر
        rb.linearVelocity = new Vector2(direction.x * moveSpeed, rb.linearVelocity.y);

        if (direction.x > 0.1f)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (direction.x < -0.1f)
            transform.localScale = new Vector3(1, 1, 1);

        animator.SetFloat("Speed", Mathf.Abs(direction.x));

        // بررسی فاصله برای حمله
        if (distanceToPlayer <= attackRange)
        {
            StartCoroutine(PerformAttack());
        }
    }

    public void OnIdleAnimationEnd()
    {
        if (!hasPlayedInitialIdle)
        {
            StartCoroutine(EnableMovementAfterDelay());
        }
    }

    private IEnumerator EnableMovementAfterDelay()
    {
        yield return new WaitForSeconds(idleToMoveDelay);
        isIdleAnimationFinished = true;
        hasPlayedInitialIdle = true;
        StartCoroutine(StartMovementAfterDelay());
    }

    private IEnumerator StartMovementAfterDelay()
    {
        yield return new WaitForSeconds(movementDelayAfterIdle);
        canMove = true;
    }

    private IEnumerator PerformAttack()
    {
        isAttacking = true;

        // غیرفعال کردن حرکت در زمان حمله
        rb.linearVelocity = Vector2.zero;

        animator.SetTrigger("Attack");
        animator.SetFloat("Speed", 0f);

        // فعال‌کردن کولایدر سلاح
        if (attackEnemy != null)
        {
            attackEnemy.ActivateEnemyWeaponCollider();
        }

        yield return new WaitForSeconds(1f); // زمان بین هر حمله
        isAttacking = false;
    }
}