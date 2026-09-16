using UnityEngine;
using TMPro;

/// <summary>
/// Отображает текущее состояние прочности объекта в формате «текущее / максимальное».
/// Принимает значения прочности через метод Display и округляет их до целых чисел.
/// </summary>

public class ObjectDurabilityView : MonoBehaviour
{
    [SerializeField] private TMP_Text _textComponent;

    public void Display(float maxDurability, float currentDurability)
    {
        int current = Mathf.RoundToInt(currentDurability);
        int max = Mathf.RoundToInt(maxDurability);

        _textComponent.text = $"{current} / {max}";
    }
}