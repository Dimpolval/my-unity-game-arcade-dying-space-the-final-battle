using UnityEngine;

/// <summary>
/// Реализует плавное следование камеры за заданной целью.
/// Использует смещение (_offset) относительно позиции цели и сглаживание движения
/// через Vector3.Lerp с настраиваемой скоростью (_smoothSpeed).
/// Требует указания цели (_target) в инспекторе для корректной работы.
/// </summary>

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;

    [SerializeField] private Vector3 _offset = new Vector3(0f, 0f, -25f);

    [SerializeField] private float _smoothSpeed = 0.125f;

    private void LateUpdate()
    {
        if (_target != null)
        {
            Vector3 desiredPosition = _target.position + _offset;
            Vector3 smoothedPosition = Vector3.Lerp(
                transform.position,
                desiredPosition,
                _smoothSpeed
            );
            transform.position = smoothedPosition;
        }
    }
}