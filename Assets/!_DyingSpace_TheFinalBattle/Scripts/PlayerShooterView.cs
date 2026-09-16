using UnityEngine;
using TMPro;

/// <summary>
/// Компонент отображает количество выстрелов в виде текста «текущее / максимальное».
/// </summary>
public class PlayerShooterView : MonoBehaviour
{
    [SerializeField] private TMP_Text _textComponent;

    public void Display(int maxShoots, int currentShoots)
    {
        _textComponent.text = $"{currentShoots} / {maxShoots}";
    }
}