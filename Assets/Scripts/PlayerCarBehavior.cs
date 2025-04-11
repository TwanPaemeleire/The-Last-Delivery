using UnityEngine;

public class PlayerCarBehavior : MonoBehaviour
{
    private float _distanceToSpawnFromCar = 4.0f;

    public void EnterCar()
    {
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<Rigidbody>().Sleep();
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerLooking>().enabled = false;
    }

    public void ExitCar(Transform carTransform)
    {
        GetComponent<CapsuleCollider>().enabled = true;
        GetComponent<Rigidbody>().WakeUp();
        transform.position = carTransform.position - (carTransform.right * _distanceToSpawnFromCar);
    }
}
