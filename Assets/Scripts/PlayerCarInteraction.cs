using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCarInteraction : MonoBehaviour
{
    [SerializeField] private CinemachineBrain _cinemachineBrain;
    [SerializeField] private CinemachineCamera _playerCamera; 
    [SerializeField] private CinemachineCamera _vehicleCamera;
    [SerializeField] private TextMeshPro _enterText;
    private bool _playerIsDriving = false;
    private bool _canEnterCar = false;
    private bool _canExitCar = true;
    private PlayerCarController _playerCarController;
    private PlayerInput _playerInput;
    private PlayerMovement _playerMovement;
    private PlayerLooking _playerLooking;

    private void Start()
    {
        _enterText.enabled = false;
        _playerCarController = GetComponent<PlayerCarController>();
        _playerCarController.enabled = false;
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
                _playerCamera.gameObject.SetActive(false);
                _vehicleCamera.gameObject.SetActive(true);
                Invoke("OnCameraBlendFinished", _cinemachineBrain.DefaultBlend.Time);
            }
            else if(_playerIsDriving && _canExitCar)
            {
                _playerIsDriving = false;
                _playerInput.SwitchCurrentActionMap("Player");
                _playerCarController.enabled = false;
                _playerCamera.gameObject.SetActive(true);
                _vehicleCamera.gameObject.SetActive(false);
                Invoke("OnCameraBlendFinished", _cinemachineBrain.DefaultBlend.Time);
            }
        }
    }

    private void OnCameraBlendFinished()
    {
        if (_playerIsDriving)
        {
            _playerCarController.enabled = true;
        }
        else
        {
            _playerMovement.enabled = true;
            _playerLooking.enabled = true;
        }
    }
}
