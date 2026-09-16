using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// ”правл€ет вращением корабл€ игрока вокруг оси Z на основе ввода.
/// ѕримен€ет поворот в FixedUpdate через умножение кватернионов, 
/// учитывает deltaTime и пропускает шаг, если ввод близок к нулю.
/// </summary>

public class PlayerRotate : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 90f;

    private float rotationInput;

    public void OnRotation(InputAction.CallbackContext context)
    {
        rotationInput = context.ReadValue<float>();
    }

    private void FixedUpdate()
    {
        HandleRotation();
    }

    private void HandleRotation()
    {
        if (Mathf.Approximately(rotationInput, 0f))
            return;

        Quaternion turn = Quaternion.Euler(0f, 0f, rotationInput * rotationSpeed * Time.deltaTime);
        transform.rotation *= turn;
    }
}
