using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 10.0f;

    private Vector2 _moveInput;
    private Rigidbody _rigidBody;

    private void Start()
    {
        GetComponent<PlayerInput>().SwitchCurrentActionMap("Player");
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(_moveInput == Vector2.zero) return;
        Vector3 moveDirection = transform.right * _moveInput.x + transform.forward * _moveInput.y;
        _rigidBody.linearVelocity = moveDirection * _movementSpeed;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
        if (_moveInput == Vector2.zero)
        {
            _rigidBody.linearVelocity = Vector3.zero;
            return;
        }
        _moveInput.Normalize();
    }
}
