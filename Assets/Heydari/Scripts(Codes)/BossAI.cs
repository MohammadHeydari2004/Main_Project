//using UnityEngine;

//public class BossAI : MonoBehaviour
//{

//    public Transform pointA;
//    public Transform pointB;
//    public Animator animator;

//    public float speed = 2f;

//    public Vector2 detectZoneOffset = new Vector2(0f, 0f); // موقعیت نسبی منطقه نسبت به کارکتر
//    public Vector2 detectZoneSize = new Vector2(5f, 3f);   // اندازه منطقه (عرض × ارتفاع)
//    public LayerMask playerLayerMask;                     // لایه بازیکن

//    private Transform currentTarget;
//    private Vector3 targetPosition;
//    private string pointATag = "Boss_PointA"; // تگ نقطه A
//    private string pointBTag = "Boss_PointB"; // تگ نقطه B
//    void Start()
//    {
//        currentTarget = pointB;
//        targetPosition = currentTarget.position;
//        // فقط یکبار انیمیشن حمله را فعال کن
//        animator.SetTrigger("HeavyAttack");
//    }

//    void Update()
//    {
//        // حرکت به سمت هدف
//        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

//        // تغییر هدف وقتی به نقطه رسیدیم
//        if (transform.position == targetPosition)
//        {
//            SwitchTarget();
//        }


//    }

//    void SwitchTarget()
//    {
//        // تعویض هدف بین A و B
//        currentTarget = (currentTarget == pointB) ? pointA : pointB;
//        targetPosition = currentTarget.position;

//        Debug.Log("currentTarget 1" + currentTarget);

//        if (currentTarget == pointA)
//        {
//            Debug.Log("currentTarget 2" + currentTarget);
//            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
//            // تشخیص ورود بازیکن به منطقه
//            DetectPlayerInZone();
//        }
//        if (currentTarget == pointB)
//        {
//            Debug.Log("currentTarget 3" + currentTarget);
//            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
//            // تشخیص ورود بازیکن به منطقه
//            DetectPlayerInZone();
//        }

//        // فقط یکبار انیمیشن حمله را فعال کن
//        animator.SetTrigger("HeavyAttack");
//    }

//    void DetectPlayerInZone()
//    {
//        // محاسبه مرکز منطقه تشخیص
//        Vector2 detectionCenter = (Vector2)transform.position + detectZoneOffset;

//        // بررسی وجود کالایدر در منطقه (فقط لایه بازیکن)
//        Collider2D playerCollider = Physics2D.OverlapBox(detectionCenter, detectZoneSize, 15f, playerLayerMask);

//        if (playerCollider != null)
//        {
//            // اگر بازیکن در منطقه باشد، انیمیشن خاص را اجرا کن
//            animator.SetTrigger("QuickAttack");
//        }
//    }

//    // برای دیدن منطقه در Scene (اختیاری)
//    private void OnDrawGizmosSelected()
//    {
//        Gizmos.color = Color.red;
//        Gizmos.DrawWireCube((Vector2)transform.position + detectZoneOffset, detectZoneSize);
//    }
//}

using UnityEngine;

public class BossAI : MonoBehaviour
{
    public Animator animator;
    public float speed = 2f;

    [Header("Detection Zone")]
    public Vector2 detectZoneOffset = new Vector2(0f, 0f);
    public Vector2 detectZoneSize = new Vector2(5f, 3f);
    public LayerMask playerLayerMask;

    [Header("Waypoint Tags")]
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
        // حرکت به سمت هدف
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        float vector = Vector3.Distance(transform.position, targetPosition);
       // Debug.Log("vector :" + vector);
        // بررسی رسیدن به هدف
        if (vector < Distance)
        {
            SwitchTargetBasedOnTag();
        }
        DetectPlayerInZone();
    }

    //void SwitchTargetBasedOnTag()
    //{
    //    // تشخیص تگ هدف فعلی و تعیین هدف بعدی
    //    if (currentTarget.CompareTag(pointBTag))
    //    {
    //        currentTarget = pointA;
    //        Quaternion newrotation = transform.localRotation;
    //        newrotation.Set(transform.localRotation.x, transform.localRotation.y + 180, transform.localRotation.z, transform.localRotation.w);
    //        FlipCharacter();
    //    }
    //    else if (currentTarget.CompareTag(pointATag))
    //    {
    //        currentTarget = pointB;
    //        Quaternion newrotation = transform.localRotation;
    //        newrotation.Set(transform.localRotation.x, transform.localRotation.y + 180, transform.localRotation.z, transform.localRotation.w);
    //        FlipCharacter();
    //    }

    //    targetPosition = currentTarget.position;
    //    DetectPlayerInZone();
    //    animator.SetTrigger("HeavyAttack");
    //}

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

    System.Collections.IEnumerator SmoothRotate(float targetAngle)
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


    //void FlipCharacter()
    //{
    //    Vector3 newScale = transform.localScale;
    //    newScale.x *= -1;
    //    transform.localScale = newScale;
    //}
    void DetectPlayerInZone()
    {
        Vector2 detectionCenter = (Vector2)transform.position + detectZoneOffset;
        Collider2D playerCollider = Physics2D.OverlapBox(detectionCenter, detectZoneSize, 0f, playerLayerMask);

        // دیباگ موقعیت
        Debug.DrawRay(detectionCenter, Vector2.right * detectZoneSize.x / 2, Color.blue, 1f);
        Debug.DrawRay(detectionCenter, Vector2.up * detectZoneSize.y / 2, Color.blue, 1f);

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