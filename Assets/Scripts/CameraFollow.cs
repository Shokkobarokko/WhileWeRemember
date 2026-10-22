using UnityEngine;

public class CameraFollow: MonoBehaviour
{
    //Объект за котороым следит камера
    [SerializeField] private Transform target;
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
    }
}
