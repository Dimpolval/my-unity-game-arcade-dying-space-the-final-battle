using TMPro;
using UnityEngine;

/// <summary>
/// Отображает итоговое время выживания (из GameTimer.LastSurvivalTimeSeconds).
/// Показ времени выполняется один раз через метод ShowSurvivalTime.
/// </summary>

public class GameTimerStatistic : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textComponent;

    private bool _hasDisplayed = false;

    private void Awake()
    {
        _textComponent = GetComponent<TextMeshProUGUI>();  
    }

    public void ShowSurvivalTime()
    {
        if (_hasDisplayed) return;
        _hasDisplayed = true;

        string timeString = FormatTime(GameTimer.LastSurvivalTimeSeconds);
        _textComponent.text = timeString;
    }

    private string FormatTime(float totalSeconds)
    {
        int minutes = Mathf.FloorToInt(totalSeconds / 60f);
        int seconds = Mathf.FloorToInt(totalSeconds % 60f);
        return $"{minutes:D2}:{seconds:D2}";
    }
}