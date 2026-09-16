using System.Collections;
using UnityEngine;

/// <summary>
/// Управляет основным состоянием игры: отслеживает гибель игрока 
/// по отсутствию объекта с тегом "Player",инициирует проигрыш (OnLose), 
/// останавливает таймер, сохраняет финальную статистику,
/// очищает динамические объекты из контейнера и 
/// через заданную задержку загружает сцену статистики.
/// Работает в связке с GameStats, GameTimer и SceneSwitcher.
/// </summary>

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform _dynamicContainer;

    [SerializeField] private SceneSwitcher _sceneSwitcher;

    [SerializeField] private float _delayBeforeStats = 3.0f;

    private bool _isPlayerDead;
    public bool IsPlayerDead => _isPlayerDead;

    private void Start()
    {
        GameStats.Instance?.Reset();
    }

    private void Update()
    {
        if (_isPlayerDead) 
            return;

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            OnLose();
        }
    }

    public void OnLose()
    {
        if (_isPlayerDead) 
            return; 
        _isPlayerDead = true;

        GameTimer.Instance?.Stop();

        GameStats.Instance?.SaveFinalStats();

        ClearDynamicObjects();
        StartCoroutine(WaitAndLoadStatsScene());
    }

    private void ClearDynamicObjects()
    {
        if (_dynamicContainer == null)
            return;

        for (int i = _dynamicContainer.childCount - 1; i >= 0; i--)
        {
            Transform child = _dynamicContainer.GetChild(i);
            Destroy(child.gameObject);
        }
    }

    private IEnumerator WaitAndLoadStatsScene()
    {
        yield return new WaitForSeconds(_delayBeforeStats);
               
        _sceneSwitcher.LoadStatisticsScene();   
    }
}