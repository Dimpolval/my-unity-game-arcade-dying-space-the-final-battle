using UnityEngine;
using TMPro;

/// <summary>
/// Отображает текущее время таймера в виде строки.
/// Публичный метод SetTime позволяет обновлять отображаемый текст 
/// из GameTimer.
/// </summary>

public class GameTimerView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textComponent;

    private void Awake()
    {
        _textComponent = GetComponent<TextMeshProUGUI>();
    }

    public void SetTime(string timeString)
    {
        _textComponent.text = timeString;
    }
}