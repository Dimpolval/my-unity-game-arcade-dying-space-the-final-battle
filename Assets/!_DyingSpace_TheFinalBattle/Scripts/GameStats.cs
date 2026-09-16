using UnityEngine;

/// <summary>
/// —инглтон-компонент дл€ учЄта игровой статистики: подсчЄта убийств и очков.
/// ѕоддерживает регистрацию убийств с разным весом 
/// (большие враги†Ч†10†очков, маленькие†Ч†5), сброс текущей статистики, 
/// сохранение финальных значений в статические пол€
/// и получение текущих показателей. 
/// </summary>

public class GameStats : MonoBehaviour
{
    public static GameStats Instance { get; private set; }
        
    public static int LastKillsCount = 0;

    public static int LastScoreCount = 0;

    private int _currentKills = 0;

    private int _currentScore = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void RegisterKill(bool isBig)
    {
        _currentKills++;

        int points = isBig ? 10 : 5;
        _currentScore += points;
    }

    public void Reset()
    {
        _currentKills = 0;
        _currentScore = 0;
    }

    public void SaveFinalStats()
    {
        LastKillsCount = _currentKills;
        LastScoreCount = _currentScore;
    }

    public int GetCurrentKills() => _currentKills;
    public int GetCurrentScore() => _currentScore;
}