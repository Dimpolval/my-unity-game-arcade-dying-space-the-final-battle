using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ”правл€ет переключением между сценами: главное меню, 
/// игровой уровень, экран статистики.
/// </summary>

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private string _mainMenuSceneName = "01_MainMenu";

    [SerializeField] private string _gameLevelSceneName = "02_GameLevel";

    [SerializeField] private string _statisticsSceneSceneName = "03_StatisticsScene";

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(_mainMenuSceneName);
    }

    public void LoadGameLevel()
    {
        SceneManager.LoadScene(_gameLevelSceneName);
    }

    public void LoadStatisticsScene()
    {
        SceneManager.LoadScene(_statisticsSceneSceneName);
    }
}