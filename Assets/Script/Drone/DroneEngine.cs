using MVCs;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class DroneEngine : MonoBehaviour, IEngine
{
    [SerializeField] private float maxPower = 4f;

    public void UpdateEngine(Rigidbody rigidbody, DroneView input)
    {
        Vector3 upVector = transform.up;
        upVector.x = 0f;
        upVector.z = 0f;
        float diff = 1 - upVector.magnitude;

        float finalDiff = Physics.gravity.magnitude * diff;


        Vector3 engineForce = Vector3.zero;
        engineForce = transform.up * ((rigidbody.mass * Physics.gravity.magnitude + finalDiff) + (input.Throttle * maxPower)) / 4f;

        rigidbody.AddForce(engineForce, ForceMode.Force);
    }
}