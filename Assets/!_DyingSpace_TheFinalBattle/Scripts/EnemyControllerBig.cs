using UnityEngine;

/// <summary>
/// Управляет поведением большого врага: отслеживает игрока по тегу "Player", 
/// регулирует дистанцию (удерживает расстояние _keepDistance), вращает врага 
/// через компонент EnemyRotate и активирует стрельбу через EnemyShooter.
/// Использует отдельные компоненты для движения, вращения и стрельбы, 
/// логику поворота рассчитывает на основе угла между направлением врага и целью.
/// </summary>

public class EnemyControllerBig : MonoBehaviour
{
    [SerializeField] private EnemyMovement _movement;

    [SerializeField] private EnemyRotate _rotate;

    [SerializeField] private EnemyShooter _shooter;

    [SerializeField] private float _keepDistance = 3f;

    private Transform _target;
       
    private void FixedUpdate()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        _target = playerObj != null ? playerObj.transform : null;

        if (_target == null)
            return;

        Vector3 directionToTarget = GetDirectionToTarget(_target.position);
        float distance = Vector3.Distance(transform.position, _target.position);

        if (distance < _keepDistance)
        {
            Vector3 awayDirection = -directionToTarget;
            float rotationInput = GetRotationInput(awayDirection);
            _rotate.SetRotationInput(rotationInput);
        }
        else
        {
            float rotationInput = GetRotationInput(directionToTarget);
            _rotate.SetRotationInput(rotationInput);
        }

        _shooter.Shoot(true);
    }

    private Vector3 GetDirectionToTarget(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        if (direction.sqrMagnitude > 0.001f)
            direction = direction.normalized;
        return direction;
    }

    private float GetRotationInput(Vector3 direction)
    {
        Vector2 dir2D = direction;
        Vector2 forward2D = transform.up;
        float angle = Vector2.SignedAngle(forward2D, dir2D);

        if (angle > 0.1f) return 1f;
        if (angle < -0.1f) return -1f;
        return 0f;
    }
}