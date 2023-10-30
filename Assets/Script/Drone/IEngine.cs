using MVCs;
using UnityEngine;

public interface IEngine
{
    public void UpdateEngine(Rigidbody rigidbody, DroneView input);

    public float GetVerticalMovement();
}