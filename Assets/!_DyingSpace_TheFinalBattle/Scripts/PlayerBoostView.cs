using UnityEngine;
using TMPro;

/// <summary>
/// Отображает время перезарядки буста игрока с одним знаком после запятой.
/// </summary>

public class PlayerBoostView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textComponent;

    private void Awake()
    {
        _textComponent = GetComponent<TextMeshProUGUI>();
    }

    public void SetCooldownTime(float cooldown)
    {
        if (cooldown <= 0f)
        {
            _textComponent.text = "0.0";
            return;
        }

        _textComponent.text = cooldown.ToString("F1");
    }
}