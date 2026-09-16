using UnityEngine;

/// <summary>
/// Управляет вращением врага на основе входного значения (rotation input).
/// Положительное значение вращает по часовой стрелке, отрицательное — против.
/// Скорость вращения настраивается через rotationSpeed.
/// </summary>

public class EnemyRotate : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 90f; 

    private float _rotationInput = 0f;

    public void SetRotationInput(float input)
    {
        _rotationInput = input;
    }

    private void FixedUpdate()
    {
        HandleRotation();
    }

    private void HandleRotation()
    {
        if (Mathf.Approximately(_rotationInput, 0f))
            return;

        float angle = _rotationInput * rotationSpeed * Time.fixedDeltaTime;
        Quaternion turn = Quaternion.Euler(0f, 0f, angle);
        transform.rotation *= turn;
    }
}