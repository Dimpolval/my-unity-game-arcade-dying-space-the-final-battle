using UnityEngine;

/// <summary>
/// Синглтон-компонент для отслеживания времени выживания игрока.
/// Ведёт отсчёт в секундах, поддерживает запуск, остановку и сброс таймера,
/// сохраняет итоговое время в статическое поле LastSurvivalTimeSeconds,
/// синхронизирует отображаемое значение с GameTimerView.
/// Предоставляет методы для получения общего времени.
/// </summary>

public class GameTimer : MonoBehaviour
{
    public static GameTimer Instance { get; private set; }

    public static float LastSurvivalTimeSeconds = 0f;

    [SerializeField] private float _startTime = 0f;

    [SerializeField] private GameTimerView _timerView; 

    private float _elapsedTime;

    private bool _isRunning = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        Reset();
        _isRunning = true;
        UpdateView();
    }

    private void Update()
    {
        if (!_isRunning) 
            return;

        _elapsedTime += Time.deltaTime;
        UpdateView();
    }

    public void Stop()
    {
        _isRunning = false;
        LastSurvivalTimeSeconds = _elapsedTime; 
        UpdateView(); 
    }

    public void Reset()
    {
        _elapsedTime = _startTime;
        _isRunning = true;
        UpdateView();
    }

    private void UpdateView()
    {
        if (_timerView != null)
            _timerView.SetTime(GetFormattedTime());
    }

    public float GetTotalSeconds() => _elapsedTime;

    public string GetFormattedTime()
    {
        int minutes = Mathf.FloorToInt(_elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(_elapsedTime % 60f);
        return $"{minutes:D2}:{seconds:D2}";
    }

    public bool IsRunning() => _isRunning;
}