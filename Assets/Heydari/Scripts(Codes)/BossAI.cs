using System.Collections;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    public Animator animator;
    public float speed = 2f;

    public Vector2 detectZoneOffset = new Vector2(0f, 0f);
    public Vector2 detectZoneSize = new Vector2(5f, 3f);
    public LayerMask playerLayerMask;

    [SerializeField] private string pointATag = "Boss_PointA";
    [SerializeField] private string pointBTag = "Boss_PointB";

    [SerializeField] float Distance;

    private Transform currentTarget;
    private Transform pointA;
    private Transform pointB;
    private Vector3 targetPosition;

    void Start()
    {
        // پیدا کردن نقاط بر اساس تگ
        pointA = GameObject.FindGameObjectWithTag(pointATag).transform;
        pointB = GameObject.FindGameObjectWithTag(pointBTag).transform;

        currentTarget = pointB;
        targetPosition = currentTarget.position;
        animator.SetTrigger("HeavyAttack");
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        float vector = Vector3.Distance(transform.position, targetPosition);

        // بررسی رسیدن به هدف
        if (vector < Distance)
        {
            SwitchTargetBasedOnTag();
        }
        DetectPlayerInZone();
    }

    void SwitchTargetBasedOnTag()
    {
        if (currentTarget.CompareTag(pointBTag))
        {
            currentTarget = pointA;
            RotateCharacter(180f);
        }
        else if (currentTarget.CompareTag(pointATag))
        {
            currentTarget = pointB;
            RotateCharacter(0f);
        }

        targetPosition = currentTarget.position;
        DetectPlayerInZone();
        animator.SetTrigger("HeavyAttack");
    }
    void RotateCharacter(float targetAngle)
    {
        StartCoroutine(SmoothRotate(targetAngle));
    }

    IEnumerator SmoothRotate(float targetAngle)
    {
        float duration = 0.5f; // مدت زمان چرخش
        float elapsed = 0f;

        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);

        while (elapsed < duration)
        {
            transform.rotation = Quaternion.Slerp(
                startRotation,
                targetRotation,
                elapsed / duration
            );
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation;
    }

    void DetectPlayerInZone()
    {
        Vector2 detectionCenter = (Vector2)transform.position + detectZoneOffset;
        Collider2D playerCollider = Physics2D.OverlapBox(detectionCenter, detectZoneSize, 0f, playerLayerMask);

        //// دیباگ موقعیت
        //Debug.DrawRay(detectionCenter, Vector2.right * detectZoneSize.x / 2, Color.blue, 1f);
        //Debug.DrawRay(detectionCenter, Vector2.up * detectZoneSize.y / 2, Color.blue, 1f);

        if (playerCollider != null)
        {
            Debug.Log("y:   " + playerCollider.gameObject.name);

            animator.SetTrigger("QuickAttack");
        }
        else
        {
            Debug.Log("no  ");
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((Vector2)transform.position + detectZoneOffset, detectZoneSize);
    }
}