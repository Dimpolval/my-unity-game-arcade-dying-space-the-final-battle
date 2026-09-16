using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Управляет прочностью объекта (врага или игрока): отслеживает текущее состояние,
/// обрабатывает получение урона и восстановление, вызывает события 
/// при изменении состояния и при уничтожении 
/// (с указанием типа врага — большой/маленький).
/// При достижении нулевой прочности запускает эффекты разрушения: 
/// звук, детонацию (ShipDestructor).
/// </summary>

public class ObjectDurability : MonoBehaviour
{
    [SerializeField] private float _maxState = 100f;

    [SerializeField] private bool _isBigEnemy = false;

    [SerializeField] private UnityEvent<float, float> _onStateChanged;

    [SerializeField] private UnityEvent<bool> _onDestroyedWithType;

    private float _currentState = 100f;

    public float MaxState => _maxState;

    public float CurrentState => _currentState;

    public bool IsAlive => _currentState > 0;


    private void Awake()
    {
        Initialize();
    }

    public void SetEnemyType(bool isBig)
    {
        _isBigEnemy = isBig;
    }

    public void Initialize()
    {
        SetCurrentState(_maxState);
    }

    public void Restore(float amount)
    {
        if (IsAlive)
        {
            float restoreAmount = Mathf.Abs(amount);
            ChangeState(restoreAmount);
        }
    }

    public void KillInstantly()
    {
        if (IsAlive)
        {
            SetCurrentState(0);
        }
        else
        {
            _onDestroyedWithType.Invoke(_isBigEnemy);
        }
    }

    public void TakeDamage(float damageAmount)
    {
        if (IsAlive)
        {
            float damage = Mathf.Abs(damageAmount) * -1;
            ChangeState(damage);
        }
    }

    private void ChangeState(float amount)
    {
        float newState = _currentState + amount;
        if (!Mathf.Approximately(_currentState, newState))
        {
            SetCurrentState(newState);
        }
    }

    private void SetCurrentState(float state)
    {
        _currentState = Mathf.Clamp(state, 0, _maxState);

        _onStateChanged.Invoke(_maxState, _currentState);

        if (!IsAlive)
        {
            var audioSource = GetComponent<AudioSource>();
            if (audioSource != null)
            {
                ShipDestroyAndFireSound.PlayExplosionAtPoint(audioSource, transform.position);
            }

            _onDestroyedWithType.Invoke(_isBigEnemy);

            var destructor = GetComponent<ShipDestructor>();
            if (destructor != null)
                destructor.Detonate();
        }
    }
}