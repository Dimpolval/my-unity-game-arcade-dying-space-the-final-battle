using TMPro;
using UnityEngine;

/// <summary>
/// Отображает статистику на экране результатов: 
/// количество убийств и счёт.
/// </summary>

public class StatsScreenView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _killsText;

    [SerializeField] private TextMeshProUGUI _scoreText;

    private bool _displayed = false;

    private void Awake()
    {
        if (_killsText == null) 
            _killsText = GetComponentInChildren<TextMeshProUGUI>();

        if (_scoreText == null) 
            _scoreText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void ShowStats()
    {
        if (_displayed) return;
        _displayed = true;

        _killsText.text = GameStats.LastKillsCount.ToString();
        _scoreText.text = GameStats.LastScoreCount.ToString();
    }
}