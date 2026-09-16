using UnityEngine;

/// <summary>
/// Скрипт управляет «временем жизни» снаряда. 
/// Он автоматически уничтожает GameObject снаряда через заданное время, 
/// предотвращая накопление объектов в сцене. 
/// </summary>

public class PlasmaChargeLifetime : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 3f;

    private void Start()
    {
        Destroy(gameObject, _lifeTime);
    }

}
