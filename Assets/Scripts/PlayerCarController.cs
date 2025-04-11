using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCarController : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 10.0f;
    bool _playerIsDriving = false;
    private Vector2 _moveInput;

    private void Update()
    {
        if (_moveInput == Vector2.zero) return;
        Vector3 moveDirection = transform.right * _moveInput.x + transform.forward * _moveInput.y;
        transform.position += moveDirection * _movementSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerInput playerInput = other.GetComponent<PlayerInput>();
        if (playerInput == null) return;
        _playerIsDriving = true;
        playerInput.SwitchCurrentActionMap("Driving");
        other.GetComponent<PlayerMovement>().enabled = false;
        other.GetComponent<PlayerLooking>().enabled = false;
    }

    public void OnSteer(InputAction.CallbackContext context)
    {
        if(!_playerIsDriving) return;
        _moveInput = context.ReadValue<Vector2>();
        _moveInput.Normalize();
    }
}
