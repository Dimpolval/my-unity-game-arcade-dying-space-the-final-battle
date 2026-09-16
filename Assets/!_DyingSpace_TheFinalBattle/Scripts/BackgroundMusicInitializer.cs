using UnityEngine;

/// <summary>
/// »нициализирует фоновую музыку при запуске игры.
/// √арантирует наличие единственного объекта BackgroundMusic: 
/// если его нет Ч создаЄт из префаба,
/// помечает как неуничтожимый при смене сцен (DontDestroyOnLoad) 
/// и задаЄт корректное им€.
/// </summary>

public class BackgroundMusicInitializer : MonoBehaviour
{
    [SerializeField] private GameObject backgroundMusicPrefab;

    void Awake()
    {
        if (GameObject.Find("BackgroundMusic") == null)
        {
            GameObject instance = Instantiate(backgroundMusicPrefab);
            instance.name = "BackgroundMusic";
            DontDestroyOnLoad(instance);
        }
    }
}
