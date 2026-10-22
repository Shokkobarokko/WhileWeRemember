using UnityEngine;

public class CameraFollow: MonoBehaviour
{
    //Объект за котороым следит камера
    [SerializeField] private Transform target;
    [SerializeField] private SpriteRenderer background;
    // Скорость с которой камера догоняет робота
    [SerializeField] private float smothSpeed = 5f;

    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        
        Vector3 targetPosition = new Vector3(
            target.position.x,
            target.position.y,
            transform.position.z
        );
        //Плавно перемешает камеру к нужной позиции
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            smothSpeed * Time.deltaTime
        );

        if(background != null)
        {
            Bounds bounds = background.bounds;

            float halfHeight = GetComponent<Camera>().orthographicSize;
            float halfWidth = halfHeight * GetComponent<Camera>().aspect;

            float minX = bounds.min.x + halfWidth;
            float maxX = bounds.max.x - halfWidth;
            float minY = bounds.min.y + halfHeight;
            float maxY = bounds.max.y - halfHeight;

            float clampedX = minX > maxX
            ? bounds.center.x
            : Mathf.Clamp(transform.position.x, minX, maxX);

            float clampedY = minY > maxY
            ? bounds.center.y
            : Mathf.Clamp(transform.position.y, minY, maxY);

            transform.position = new Vector3(
                clampedX,
                clampedY,
                transform.position.z
            );
        }
    }
}
