using UnityEngine;

/// <summary>
/// Наносит урон игроку при столкновении, соблюдая кулдаун между атаками.
/// При срабатывании проверяет наличие компонента ObjectDurability у игрока 
/// и вызывает TakeDamage.
/// Величина урона и время перезарядки настраиваются в инспекторе.
/// </summary>

public class DealDamage : MonoBehaviour
{
    [SerializeField] private float _damage = 5f;

    [SerializeField] private float _cooldown = 5f;

    private float _nextAttackTime = 0f;

    private void OnCollisionEnter(Collision collision)
    {
        if (Time.time < _nextAttackTime) 
            return;

        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.TryGetComponent<ObjectDurability>(out var durability))
            {
                durability.TakeDamage(_damage);
                _nextAttackTime = Time.time + _cooldown;
            }
        }
    }
}