using UnityEngine;

/// <summary>
/// Управляет паузой игры по нажатию пробела: останавливает время, 
/// ставит на паузу звук, показывает/скрывает панель паузы.
/// </summary>

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        AudioListener.pause = true;
        pausePanel.SetActive(true);
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        pausePanel.SetActive(false);
    }
}