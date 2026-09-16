using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ”правл€ет детонацией корабл€ игрока и противников: 
/// разбрасывает обломки с физикой, отключает коллайдер и рендерер,
/// собирает список обломков и передаЄт его в DebrisCleanup 
/// дл€ последующей очистки сцены.
/// </summary>

public class ShipDestructor : MonoBehaviour
{
    [SerializeField] private float _explosionRadius = 5f;

    [SerializeField] private float _explosionForce = 10f;

    [SerializeField] private float _upwardsModifier = 0f;

    [SerializeField] private DebrisCleanup _debrisCleanup;

    private bool _isDestroyed;

    public void Detonate()
    {
        if (_isDestroyed) return;
        _isDestroyed = true;

        List<GameObject> debris = Explode();

        if (_debrisCleanup != null)
        {
            _debrisCleanup.ScheduleCleanup(debris);
        }

        gameObject.SetActive(false);
    }

    private List<GameObject> Explode()
    {
        var debris = new List<GameObject>();
        Transform[] children = GetComponentsInChildren<Transform>(true);

        foreach (Transform child in children)
        {
            if (child == transform) continue;
            if (!child.CompareTag("Debris")) continue; 

            child.parent = null;

            Rigidbody rb = child.gameObject.GetComponent<Rigidbody>();
            if (rb == null) rb = child.gameObject.AddComponent<Rigidbody>();

            rb.constraints = RigidbodyConstraints.FreezePositionZ |
                              RigidbodyConstraints.FreezeRotationX |
                              RigidbodyConstraints.FreezeRotationY;

            rb.AddExplosionForce(_explosionForce, transform.position, _explosionRadius, _upwardsModifier, ForceMode.Impulse);

            debris.Add(child.gameObject);
        }

        if (TryGetComponent(out Collider col)) col.enabled = false;
        if (TryGetComponent(out Renderer ren)) ren.enabled = false;

        return debris;
    }
}