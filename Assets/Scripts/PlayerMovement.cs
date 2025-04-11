using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 10.0f;

    private Vector2 _moveInput;

    private void Update()
    {
        if (_moveInput == Vector2.zero) return;
        Vector3 moveDirection = transform.right * _moveInput.x + transform.forward * _moveInput.y;
        transform.position += moveDirection * _movementSpeed * Time.deltaTime;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
        _moveInput.Normalize();
    }
}
