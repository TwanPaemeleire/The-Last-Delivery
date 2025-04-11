using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLooking : MonoBehaviour
{
    [SerializeField] private Transform _cameraParent;
    private float _cameraMoveMultiplier = 0.2f;

    private float _xRotation = 0f;

    public void OnLooking(InputAction.CallbackContext context)
    {
        Vector2 lookInput = context.ReadValue<Vector2>();
        
        float mouseX = lookInput.x * _cameraMoveMultiplier;
        float mouseY = lookInput.y * _cameraMoveMultiplier;
        
        // Rotate Up/Down (Pitch)
        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
        
        _cameraParent.localEulerAngles = new Vector3(_xRotation, 0f, 0f);
        
        // Rotate Entire Player Left/Right (Yaw)
        transform.Rotate(Vector3.up * mouseX);
    }
}
