using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Управляет очисткой обломков (debris) через заданное время.
/// Принимает список объектов через ScheduleCleanup, 
/// планирует их удаление через Invoke,
/// затем уничтожает все объекты из списка в методе DestroyDebris и очищает список.
/// Время жизни обломков настраивается в инспекторе.
/// </summary>

public class DebrisCleanup : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 1f;

    private List<GameObject> _debrisToRemove = new List<GameObject>();
        
    public void ScheduleCleanup(List<GameObject> debris)
    {
        _debrisToRemove.Clear();
        _debrisToRemove.AddRange(debris);

        Invoke(nameof(DestroyDebris), _lifeTime);
    }

    private void DestroyDebris()
    {
        foreach (var obj in _debrisToRemove)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }
        _debrisToRemove.Clear();
    }
}