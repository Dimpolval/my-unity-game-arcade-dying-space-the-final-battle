using UnityEngine;

/// <summary>
/// ”правл€ет спавном врагов из портала: случайным образом 
/// выбирает между двум€ типами врагов, соблюдает интервал спавна 
/// в заданном диапазоне и размещает новых врагов в динамическом контейнере.
/// ѕри спавне передаЄт ссылку на DynamicCharge компоненту EnemyShooter.
/// —павн отключаетс€, если не настроены ключевые ссылки в инспекторе 
/// или отсутствует игрок.
/// </summary>

public class EnemyPortal : MonoBehaviour
{
    [SerializeField] private GameObject _enemyTypeA;

    [SerializeField] private GameObject _enemyTypeB;

    [SerializeField] private float _minSpawnInterval = 1.5f;

    [SerializeField] private float _maxSpawnInterval = 3.0f;
    
    [SerializeField] private Transform _dynamicContainer;

    private Transform _dynamicCharge;

    private float _nextSpawnTime;

    private bool _isSpawningEnabled = true;

    private void Awake()
    {
        _dynamicCharge = GameObject.Find("DynamicCharge").transform;
    }

    private void Start()
    {
        if (_dynamicContainer == null || _enemyTypeA == null || _enemyTypeB == null)
        {
            _isSpawningEnabled = false;
            return;
        }

        _nextSpawnTime = Time.time + Random.Range(_minSpawnInterval, _maxSpawnInterval);
    }

    private void Update()
    {
        if (!_isSpawningEnabled) 
            return;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) 
            return;

        if (Time.time >= _nextSpawnTime)
        {
            SpawnRandomEnemy();
            _nextSpawnTime = Time.time + Random.Range(_minSpawnInterval, _maxSpawnInterval);
        }
    }

    private void SpawnRandomEnemy()
    {
        GameObject enemyPrefab = Random.value < 0.5f ? _enemyTypeA : _enemyTypeB;

        Vector3 spawnPosition = transform.position;
        
        Quaternion spawnRotation = Quaternion.Euler(0f, 0f, transform.eulerAngles.z);

        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, spawnRotation);
                
        enemy.transform.SetParent(_dynamicContainer, worldPositionStays: true);

        var shooter = enemy.GetComponent<EnemyShooter>();
        if (shooter != null)
            shooter.DynamicCharge = _dynamicCharge;
    }
}