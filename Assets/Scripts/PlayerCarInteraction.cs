using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCarInteraction : MonoBehaviour
{
    [SerializeField] private TextMeshPro _enterText;
    private bool _playerIsDriving = false;
    private bool _canEnterCar = false;
    private bool _canExitCar = false;
    private PlayerCarController _playerCarController;
    private PlayerInput _playerInput;
    private PlayerMovement _playerMovement;
    private PlayerLooking _playerLooking;

    private void Start()
    {
        _enterText.enabled = false;
        _playerCarController = GetComponent<PlayerCarController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _playerInput = other.GetComponent<PlayerInput>();
        _playerMovement = other.GetComponent<PlayerMovement>();
        _playerLooking = other.GetComponent<PlayerLooking>();
        if (_playerInput == null) return;
        _enterText.enabled = true;
        _canEnterCar = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _playerInput = other.GetComponent<PlayerInput>();
        if (_playerInput == null) return;
        _enterText.enabled = false;
    }

    public void OnEnterOrExitCar(InputAction.CallbackContext context)
    {
        if(context.canceled)
        {
            if(!_playerIsDriving && _canEnterCar)
            {
                _playerIsDriving = true;
                _playerInput.SwitchCurrentActionMap("Driving");
                _playerMovement.enabled = false;
                 _playerLooking.enabled = false;
                _playerCarController.enabled = true;
            }
            else if(_playerIsDriving && _canExitCar)
            {
                _playerIsDriving = false;
                _playerInput.SwitchCurrentActionMap("Player");
                _playerMovement.enabled = true;
                _playerLooking.enabled = true;
                _playerCarController.enabled = false;
            }
        }
    }
}
