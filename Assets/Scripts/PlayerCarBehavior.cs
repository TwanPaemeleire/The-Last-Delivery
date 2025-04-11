using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCarBehavior : MonoBehaviour
{
    private float _distanceToSpawnFromCar = 4.0f;

    public void EnterCar()
    {
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<Rigidbody>().Sleep();
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerLooking>().enabled = false;
        GetComponent<PlayerInput>().SwitchCurrentActionMap("Driving");
    }

    public void ExitCar(Transform carTransform)
    {
        GetComponent<PlayerInput>().SwitchCurrentActionMap("Player");
        GetComponent<CapsuleCollider>().enabled = true;
        GetComponent<Rigidbody>().WakeUp();
        transform.position = carTransform.position - (carTransform.right * _distanceToSpawnFromCar);
    }

    public void OnCameraReachedPlayer()
    {
        GetComponent<PlayerMovement>().enabled = true;
        GetComponent<PlayerLooking>().enabled = true;
    }
}
