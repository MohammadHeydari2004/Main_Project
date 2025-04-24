using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;         // کاراکتر
    public float smoothSpeed = 0.125f; // سرعت نرمی حرکت
    public float minX = -10f;        // حداقل موقعیت دوربین
    public float maxX = 10f;         // حداکثر موقعیت دوربین
    public float yPosition = 0f;     // موقعیت ثابت Y
    public float zPosition = -10f;   // موقعیت ثابت Z (برای دوربین)

    void LateUpdate()
    {
        if (target != null)
        {
            float desiredX = Mathf.Clamp(target.position.x, minX, maxX);
            float smoothedX = Mathf.Lerp(transform.position.x, desiredX, smoothSpeed);

            transform.position = new Vector3(smoothedX, yPosition, zPosition);
        }
    }
}
