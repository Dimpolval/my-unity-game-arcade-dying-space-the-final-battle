using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

/// <summary>
/// ”правл€ет стрельбой игрока: контролирует количество выстрелов,
/// восстанавливает патроны с заданным интервалом, запускает событие дл€ UI
/// и создаЄт плазменные зар€ды из префаба в указанных точках огн€ (_firePoints).
/// —трельба возможна только пока игрок жив (провер€етс€ через GameManager).
/// </summary>

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private Transform[] _firePoints;

    [SerializeField] private GameObject _plasmaChargePrefab;

    [SerializeField] private int _maxShoots = 14;

    [SerializeField] private float _refillInterval = 1f;

    [SerializeField] private Transform _dynamicCharge;

    [SerializeField] private GameManager _gameManager;

    private int _currentShoots;

    private float _refillTimer;

    private bool IsAlive => _gameManager != null && !_gameManager.IsPlayerDead;

    public UnityEvent<int, int> OnShootsChanged;

    private void OnEnable()
    {
        _currentShoots = _maxShoots;
        _refillTimer = 0f;
        UpdateView();
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (!IsAlive)
        {
            return;
        }

        if (_firePoints == null || _firePoints.Length == 0)
        {
            return;
        }

        if (context.performed && _currentShoots > 0)
        {
            ShootOnce();
            _currentShoots--;
            UpdateView();
        }
    }

    private void Update()
    {
        if (IsAlive && _currentShoots < _maxShoots)
        {
            _refillTimer += Time.deltaTime;
            if (_refillTimer >= _refillInterval)
            {
                _currentShoots++;
                _refillTimer -= _refillInterval;
                if (_currentShoots > _maxShoots)
                {
                    _currentShoots = _maxShoots;
                }
                UpdateView();
            }
        }
        else
        {
            _refillTimer = 0f;
        }
    }

    private void ShootOnce()
    {
        if (_firePoints == null) return;

        foreach (Transform firePoint in _firePoints)
        {
            if (firePoint == null)
            {
                continue; 
            }

            Vector3 spawnPosition = firePoint.position;
            Quaternion plasmaChargeDirection = firePoint.rotation;

            Instantiate(_plasmaChargePrefab, spawnPosition, plasmaChargeDirection, _dynamicCharge);
        }
    }

    private void UpdateView()
    {
        OnShootsChanged?.Invoke(_maxShoots, _currentShoots);
    }
}