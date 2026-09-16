using UnityEngine;

/// <summary>
/// Управляет постепенным уничтожением планет: случайным образом выбирает 
/// планету из списка, создаёт эффект разрушения через префаб, 
/// удаляет планету и обновляет список оставшихся.
/// Интервалы между разрушениями выбираются случайно в заданном диапазоне.
/// </summary>

public class PlanetDestructor : MonoBehaviour
{
    [SerializeField] private Transform[] _planets;

    [SerializeField] private GameObject _destructionPrefab;

    [SerializeField] private float _minInterval = 15f;

    [SerializeField] private float _maxInterval = 20f;

    private float _nextDestructionTime;

    private void Awake()
    {
        if (_planets == null || _planets.Length == 0)
        {
            GameObject[] planetObjects = GameObject.FindGameObjectsWithTag("Planet");
            _planets = new Transform[planetObjects.Length];
            for (int i = 0; i < planetObjects.Length; i++)
                _planets[i] = planetObjects[i].transform;
        }
    }

    private void Update()
    {
        if (Time.time >= _nextDestructionTime && _planets.Length > 0 && _destructionPrefab != null)
        {
            DestroyPlanet();
            SetNextDestruction();
        }
    }

    private void DestroyPlanet()
    {
        int randomIndex = UnityEngine.Random.Range(0, _planets.Length);
        Transform targetPlanet = _planets[randomIndex];

        if (targetPlanet == null) 
            return;

        Instantiate(_destructionPrefab, targetPlanet.position, targetPlanet.rotation);

        Destroy(targetPlanet.gameObject);

        _planets[randomIndex] = _planets[_planets.Length - 1];
        System.Array.Resize(ref _planets, _planets.Length - 1);
    }

    private void SetNextDestruction()
    {
        float randomInterval = UnityEngine.Random.Range(_minInterval, _maxInterval);
        _nextDestructionTime = Time.time + randomInterval;
    }
}