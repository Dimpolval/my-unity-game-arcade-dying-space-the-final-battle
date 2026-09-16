using UnityEngine;

/// <summary>
/// Управляет поведением маленького врага: отслеживает игрока по тегу "Player",
/// вращает врага через компонент EnemyRotate 
/// и управляет стрельбой через EnemyShooter.
/// Отключает стрельбу, если враг подошёл ближе заданного расстояния 
/// (_stopShootingAtDistance), чтобы избежать избыточных выстрелов в упор.
/// Логика поворота основана на угле между направлением врага и позицией цели.
/// </summary>

public class EnemyControllerSmall : MonoBehaviour
{
    [SerializeField] private EnemyMovement _movement;

    [SerializeField] private EnemyRotate _rotate;

    [SerializeField] private EnemyShooter _shooter;

    [SerializeField] private float _stopShootingAtDistance = 2f;

    private Transform _target;

    private void FixedUpdate()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        _target = playerObj != null ? playerObj.transform : null;

        if (_target == null)
            return;

        Vector3 directionToTarget = GetDirectionToTarget(_target.position);
        float distance = Vector3.Distance(transform.position, _target.position);

        float rotationInput = GetRotationInput(directionToTarget);
        _rotate.SetRotationInput(rotationInput);

        bool shouldShoot = true;
        if (_stopShootingAtDistance > 0f && distance < _stopShootingAtDistance)
        {
            shouldShoot = false;
        }

        if (shouldShoot)
        {
            _shooter.Shoot(true);
        }
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