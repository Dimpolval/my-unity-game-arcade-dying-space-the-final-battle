using UnityEngine;

/// <summary>
/// Управляет движением корабля игрока: применяет тягу двигателя 
/// через AddForceAtPosition, ограничивает максимальную скорость и 
/// предоставляет доступ к текущей скорости и настройкам.
/// Поддерживает временное отключение взаимодействия (IsInteractive)
/// и предотвращает засыпание Rigidbody.
/// </summary>

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Transform _enginePosition;

    [SerializeField] private Rigidbody _rigidbody;

    [SerializeField] private float _autoThrust = 8f;

    [SerializeField] private float _maxSpeed = 20f;

    public float MaxSpeed
    {
        get => _maxSpeed;
        set => _maxSpeed = value;
    }

    public Vector3 Velocity => _rigidbody.velocity;

    public bool IsInteractive { get; set; }

    private void Awake() => IsInteractive = true;

    private void Start() => _rigidbody.sleepThreshold = 0.0f;

    private void FixedUpdate()
    {
        if (!IsInteractive) 
            return;

        _rigidbody.AddForceAtPosition(
            transform.up * _autoThrust,
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