using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCarController : MonoBehaviour
{
    private float _maxSpeed = 50.0f;
    private float _maxReverseSpeed = -5.0f;
    private float _accelerationSpeed  = 20.0f;
    private float _breakSpeed = 15.0f;
    private float _decelerationSpeed = 15.0f;
    private float _turnSpeed = 90.0f;
    private float _currentSpeed;
    private Rigidbody _rigidbody;
    private Vector2 _moveInput;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = false;
    }

    private void Update()
    {
        // Rotate The Car
        transform.Rotate(Vector3.up * _moveInput.x * _turnSpeed * Time.deltaTime);

        if (_moveInput.y > 0.0f && _currentSpeed < _maxSpeed) // Holding W -> Accelerate (If Possible)
        {
            _currentSpeed += _accelerationSpeed * Time.deltaTime;
            if (_currentSpeed > _maxSpeed) _currentSpeed = _maxSpeed;
        }
        else if (_moveInput.y < 0.0f && _currentSpeed > _maxReverseSpeed) // Holding S -> Brake
        {
            _currentSpeed -= _breakSpeed * Time.deltaTime;
            if (_currentSpeed < _maxReverseSpeed) _currentSpeed = _maxReverseSpeed;
        }
        else // There Was No Accelerating Or Breaking, So Slowly Decelerate
        {
            if (_currentSpeed > 0.0f)
            {
                _currentSpeed -= _decelerationSpeed * Time.deltaTime;
                if (_currentSpeed < 0.0f) _currentSpeed = 0.0f;
            }
            else if (_currentSpeed < 0.0f)
            {
                _currentSpeed += _decelerationSpeed * Time.deltaTime;
                if (_currentSpeed > 0.0f) _currentSpeed = 0.0f;
            }
        }
        Vector3 newVelocity = transform.forward * _currentSpeed;
        _rigidbody.linearVelocity = newVelocity;
    }

    private void OnEnable()
    {
        _rigidbody.constraints = RigidbodyConstraints.None;
        _rigidbody.isKinematic = false;
    }
    private void OnDisable()
    {
        _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        _rigidbody.isKinematic = true;
    }

    public void OnSteer(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
        _moveInput.Normalize();
    }

    public void OnLooking(InputAction.CallbackContext context)
    {

    }

    public void OnAscend(InputAction.CallbackContext context)
    {

    }

    public void OnDescend(InputAction.CallbackContext context)
    {

    }
}
