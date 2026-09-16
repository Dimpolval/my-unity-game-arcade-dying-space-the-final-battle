using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Управляет механикой ускорения игрока: активирует повышенную скорость 
/// на заданное время, затем ставит способность на перезарядку. 
/// Синхронизирует состояние с видом (PlayerBoostView)
/// и взаимодействует с компонентом движения (PlayerMove) 
/// для изменения максимальной скорости.
/// </summary>

public class PlayerBoost : MonoBehaviour
{
    [SerializeField] private float _boostMultiplier = 1.75f;

    [SerializeField] private float _boostDuration = 2f;

    [SerializeField] private float _cooldown = 5f;

    [SerializeField] private PlayerMove _playerMove;

    [SerializeField] private PlayerBoostView _boostView;

    private float _boostTimer;

    private float _cooldownTimer;

    private bool _isBoostActive;

    private float _originalMaxSpeed;

    public void OnBoost(InputAction.CallbackContext context)
    {
        if (context.performed && !_isBoostActive && _cooldownTimer <= 0f)
            ActivateBoost();
    }

    private void Awake()
    {
        _originalMaxSpeed = _playerMove.MaxSpeed;
    }

    private void Update()
    {
        if (_isBoostActive)
        {
            _boostTimer -= Time.deltaTime;
            if (_boostTimer <= 0f)
                DeactivateBoost();
        }
        else if (_cooldownTimer > 0f)
        {
            _cooldownTimer -= Time.deltaTime;
        }

        _boostView.SetCooldownTime(_cooldownTimer);
    }

    private void ActivateBoost()
    {
        _isBoostActive = true;
        _boostTimer = _boostDuration;
        _cooldownTimer = 0f;
        _playerMove.MaxSpeed = _originalMaxSpeed * _boostMultiplier;
    }

    private void DeactivateBoost()
    {
        _isBoostActive = false;
        _boostTimer = 0f;
        _cooldownTimer = _cooldown;
        _playerMove.MaxSpeed = _originalMaxSpeed;
    }     
}