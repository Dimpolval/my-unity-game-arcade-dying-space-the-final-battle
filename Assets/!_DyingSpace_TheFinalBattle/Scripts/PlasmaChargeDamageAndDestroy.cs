using UnityEngine;

/// <summary>
/// Наносит урон при столкновении объектам с компонентом ObjectDurability,
/// учитывает тип снаряда (вражеский или нет) и слои, чтобы не наносить урон «своим».
/// Уничтожает сам себя после срабатывания.
/// </summary>

public class PlasmaChargeDamageAndDestroy : MonoBehaviour
{
    [SerializeField] private float _damage = 10f;

    private int _enemyProjectileLayer;

    private int _playerLayer;

    private int _enemyLayer;

    private void Awake()
    {
        _enemyProjectileLayer = LayerMask.NameToLayer("Projectile"); 
        _playerLayer = LayerMask.NameToLayer("Player");
        _enemyLayer = LayerMask.NameToLayer("Enemy");
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;

        if (other.layer == _enemyProjectileLayer)
            return;

        if (IsEnemyProjectile() && other.layer == _enemyLayer)
            return;

        if (!IsEnemyProjectile() && other.layer == _playerLayer)
            return;

        if (other.TryGetComponent<ObjectDurability>(out var durability))
        {
            durability.TakeDamage(_damage);
        }

        Destroy(gameObject);
    }

    private bool IsEnemyProjectile()
    {
        return gameObject.GetComponent<EnemyProjectileMarker>() != null;
    }
}