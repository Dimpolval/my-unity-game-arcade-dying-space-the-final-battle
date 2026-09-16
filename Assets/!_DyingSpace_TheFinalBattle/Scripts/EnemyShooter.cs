using System.Collections;
using UnityEngine;

/// <summary>
/// ”правл€ет стрельбой врага: запускает корутину дл€ периодических выстрелов,
/// выбирает случайный интервал между выстрелами в заданном диапазоне и создаЄт
/// плазменные зар€ды из префаба в указанных точках огн€ (_firePoints).
/// ѕоддерживает передачу ссылки на DynamicCharge дл€ прив€зки снар€дов.
/// </summary>

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private Transform[] _firePoints;

    [SerializeField] private GameObject _plasmaChargePrefab;

    [SerializeField] private float _minShootInterval = 1.0f;

    [SerializeField] private float _maxShootInterval = 2.5f;

    [SerializeField] public Transform _dynamicCharge;

    public Transform DynamicCharge
    {
        set => _dynamicCharge = value;
    }

    private bool _canShoot = false;

    private void Awake()
    {
        if (_firePoints == null || _firePoints.Length == 0)
            _firePoints = new[] { transform };
    }

    private void OnEnable()
    {
        StartCoroutine(ShootCoroutine());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public void Shoot(bool canShoot)
    {
        _canShoot = canShoot;
    }

    private IEnumerator ShootCoroutine()
    {
        while (true)
        {
            if (_canShoot)
            {
                ShootOnce();
            }

            float delay = Random.Range(_minShootInterval, _maxShootInterval);
            yield return new WaitForSeconds(delay);
        }
    }

    private void ShootOnce()
    {
        if (_plasmaChargePrefab == null)
            return;

        foreach (Transform firePoint in _firePoints)
        {
            Vector3 spawnPosition = firePoint.position;
            Quaternion spawnRotation = firePoint.rotation;

            GameObject bullet = Instantiate(_plasmaChargePrefab, spawnPosition, spawnRotation, _dynamicCharge);

            bullet.AddComponent<EnemyProjectileMarker>();
        }
    }
}