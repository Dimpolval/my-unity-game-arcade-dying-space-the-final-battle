using UnityEngine;

/// <summary>
/// Управляет движением врага через Rigidbody: прикладывает тягу в точке двигателя 
/// (_enginePosition) и ограничивает максимальную скорость (_maxSpeed). 
/// Предоставляет доступ к текущей скорости через свойство Velocity 
/// и позволяет менять лимит скорости через свойство MaxSpeed.
/// В Awake отключает сон Rigidbody (sleepThreshold = 0), 
/// чтобы физика работала стабильно.
/// </summary>

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform _enginePosition;

    [SerializeField] private Rigidbody _rigidbody;

    [SerializeField] private float _thrustForce = 8f;

    [SerializeField] private float _maxSpeed = 20f;

    public float MaxSpeed
    {
        get => _maxSpeed;
        set => _maxSpeed = value;
    }

    public Vector3 Velocity => _rigidbody.velocity;

    private void Awake()
    {
        
        _rigidbody.sleepThreshold = 0.0f;
    }

    private void FixedUpdate()
    {
        _rigidbody.AddForceAtPosition(
            transform.up * _thrustForce,
            _enginePosition.position,
            ForceMode.Acceleration
        );

        float currentSpeed = _rigidbody.velocity.magnitude;
        if (currentSpeed > _maxSpeed)
        {
            Vector3 direction = _rigidbody.velocity.normalized;
            _rigidbody.velocity = direction * _maxSpeed;
        }
    }
}