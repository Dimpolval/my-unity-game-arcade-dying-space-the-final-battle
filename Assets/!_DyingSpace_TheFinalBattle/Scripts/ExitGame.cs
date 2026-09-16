using UnityEngine;

/// <summary>
/// Обеспечивает выход из игры по вызову.
/// Закрывает приложение только в сборке Build.
/// </summary>

public class ExitGame : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }
}