using UnityEngine;

/// <summary>
/// »нициализирует отображение статистики на экране результатов: 
/// запрашивает у GameTimerStatistic
/// врем€ выживани€ и у StatsScreenView Ч полный набор статистики.
/// »спользует FindObjectOfType дл€ поиска нужных компонентов в сцене.
/// </summary>

public class StatsSceneInit : MonoBehaviour
{
    private void Start()
    {
        var timerStat = FindObjectOfType<GameTimerStatistic>();
        var statsView = FindObjectOfType<StatsScreenView>();

        if (timerStat != null)
            timerStat.ShowSurvivalTime();

        if (statsView != null)
            statsView.ShowStats();
    }
}