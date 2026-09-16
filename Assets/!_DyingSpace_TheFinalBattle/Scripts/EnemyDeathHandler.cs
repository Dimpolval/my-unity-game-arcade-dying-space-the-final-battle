using UnityEngine;

/// <summary>
/// Обрабатывает событие уничтожения врага: передаёт информацию о типе врага 
/// (большой/маленький) в GameStats для учёта статистики. 
/// </summary>

public class EnemyDeathHandler : MonoBehaviour
{
    public void OnEnemyDestroyed(bool isBigEnemy)
    {
        GameStats.Instance?.RegisterKill(isBigEnemy);
    }
}