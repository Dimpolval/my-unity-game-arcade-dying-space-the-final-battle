using UnityEngine;

/// <summary>
/// —крипт отвечает за инициализацию движени€ снар€да сразу после его создани€.
/// ќн задаЄт посто€нную скорость снар€ду в направлении, 
/// в котором тот был выпущен (его ось Z).
/// </summary>

public class PlasmaChargeMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;

    [SerializeField] private float _speed = 5;

    private void Start()
    {
        _rigidbody.velocity = transform.forward * _speed;
    }
}
