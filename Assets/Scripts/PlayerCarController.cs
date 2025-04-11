using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCarController : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 10.0f;
    private Rigidbody _rigidbody;
    private Vector2 _moveInput;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
    }

    private void Update()
    {
        if (_moveInput == Vector2.zero) return;
        Vector3 moveDirection = transform.right * _moveInput.x + transform.forward * _moveInput.y;
        transform.position += moveDirection * _movementSpeed * Time.deltaTime;
    }

    public void OnSteer(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
        _moveInput.Normalize();
    }
}
